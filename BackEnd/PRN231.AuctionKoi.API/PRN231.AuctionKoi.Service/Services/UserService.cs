using AutoMapper;
using KoiAuction.BussinessModels.UserAuctionModels;
using KoiAuction.BussinessModels.UserModels;
using KoiAuction.Common;
using KoiAuction.Common.Utils;
using KoiAuction.Repository.Entities;
using KoiAuction.Service.Base;
using KoiAuction.Service.ISerivice;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PRN231.AuctionKoi.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Net.WebSockets;
using System.Security.Cryptography;
using KoiAuction.Common.Enums;

namespace KoiAuction.Service.Services
{
    public class UserService : IUserService
    {
        public IUnitOfWork _unitOfWork;
        public IMapper _mapper;
        public IConfiguration _configuration;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<IBusinessResult> CheckLogin(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return new BusinessResult(Const.WARNING_INVALID_LOGIN_CODE, Const.WARNING_INVALID_LOGIN_MSG);
            }
            Expression<Func<User, bool>> filter = null!;
            filter = x => x.Mail!.Equals(email) && x.Password!.Equals(PasswordHelper.ConvertToEncrypt(password));
            var userEntity = await _unitOfWork.UserRepository.GetByCondition(filter, includeProperties: "Role");
            if (userEntity == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            var getRefreshToken = await _unitOfWork.RefreshTokenRepository.GetByCondition(x => x.UserId == userEntity.UserId);
           
            var userModel = _mapper.Map<UserModel>(userEntity);
            if (getRefreshToken == null)
            {
                userModel.AccessToken = GenerateAccessToken(userEntity);
                userModel.RefreshToken = GenerateRefreshToken(userEntity);
                var refreshTokenObj = new RefreshToken
                {
                    JwtId = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    RefreshTokenCode = PasswordHelper.ConvertToEncrypt(userModel.RefreshToken),
                    UserId = userEntity.UserId,
                    RefreshTokenValue = userModel.RefreshToken,
                    ExpiredAt = GetExpireDateFromToken(userModel.RefreshToken)

                };
                await _unitOfWork.RefreshTokenRepository.Insert(refreshTokenObj);
            } 
            else if(getRefreshToken.ExpiredAt < DateTime.UtcNow)
            {
                userModel.AccessToken = GenerateAccessToken(userEntity);
                userModel.RefreshToken = GenerateRefreshToken(userEntity);
                getRefreshToken.RefreshTokenValue = userModel.RefreshToken;
                _unitOfWork.RefreshTokenRepository.Update(getRefreshToken);
            }
            else
            {
                userModel.AccessToken = GenerateAccessToken(userEntity);
                userModel.RefreshToken = getRefreshToken.RefreshTokenValue;
            }                                                 
            await _unitOfWork.SaveAsync();
           
            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,userModel);
        }

        public string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Mail),
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.RoleName.ToString()),
                }),
                Expires = DateTime.UtcNow.AddSeconds(int.Parse(_configuration["JWT:AccessTokenExpire"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"]
            };
            var token =  tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Mail),
                    new Claim(ClaimTypes.Role, user.Role.RoleName.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(int.Parse(_configuration["JWT:RefreshTokenExpire"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public DateTime? GetExpireDateFromToken(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(refreshToken) as JwtSecurityToken;

            // Kiểm tra xem token có hợp lệ không
            if (jwtToken == null)
            {
                return null;
            }

            // Lấy giá trị Expiration từ token
            return jwtToken.ValidTo;
        }
        public async Task<IBusinessResult> Register(string email, string password, UserRole userRole)
        {
            try
            {
                var checkExistUser = await _unitOfWork.UserRepository.GetByCondition(x => x.Mail.Equals(email), includeProperties: "Role");
                if (checkExistUser == null)
                {
                    string decodePassword = PasswordHelper.ConvertToEncrypt(password);
                    User user = new User
                    {
                        Mail = email,
                        FullName = email,
                        Password = decodePassword,
                        RoleId = (int)userRole,
                        CreateDate = DateTime.Now
                    };
                    
                    await _unitOfWork.UserRepository.Insert(user);
                    await _unitOfWork.SaveAsync();

                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, "Register success. You can login right now!");
                }
                return new BusinessResult(Const.FAIL_CREATE_CODE, "User is exist", new List<string>());
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, Const.FAIL_READ_MSG, ex.ToString());
            }
        }

        public async Task<IBusinessResult> CheckRefreshToken(string refreshToken)
        {
            var token = await _unitOfWork.RefreshTokenRepository.GetTokenByRefreshToken(refreshToken);
            if(token == null)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, "Refresh Token does not exist", null);
            }
            if(token.ExpiredAt < DateTime.UtcNow)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, "Refresh Token expired. Please log out", null);
            }

            var user = await _unitOfWork.UserRepository.GetByCondition(x => x.UserId == token.UserId, includeProperties: "Role");
            var newAccessToken = GenerateAccessToken(user);
            return new BusinessResult(Const.SUCCESS_CREATE_CODE, "Access Token generated successfully", newAccessToken);
        }

    }
}
