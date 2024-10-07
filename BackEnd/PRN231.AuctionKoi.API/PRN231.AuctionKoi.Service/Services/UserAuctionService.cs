using AutoMapper;
using KoiAuction.BussinessModels.Pagination;
using KoiAuction.BussinessModels.PaymentModels;
using KoiAuction.BussinessModels.Proposal;
using KoiAuction.BussinessModels.UserAuctionModels;
using KoiAuction.Common;
using KoiAuction.Common.Utils;
using KoiAuction.Common.Utils.Filters;
using KoiAuction.Repository.Entities;
using KoiAuction.Service.Base;
using KoiAuction.Service.ISerivice;
using KoiAuction.Service.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using PRN231.AuctionKoi.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Service.Services
{
    public class UserAuctionService : IUserAuctionService
    {
        public IUnitOfWork _unitOfWork;
        public IMapper _mapper;

        public UserAuctionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IBusinessResult> Get(PaginationParameter paginationParameter, UserAuctionFilters userAuctionFilters)
        {
            Expression<Func<UserAuction, bool>> filter = null!;

            Func<IQueryable<UserAuction>, IOrderedQueryable<UserAuction>> orderBy = null!;
            if (!(paginationParameter.Search == null || paginationParameter.Search.Equals("")))
            {
                int validInt = 0;
                double validDouble = 0;

                var checkInt = int.TryParse(paginationParameter.Search, out validInt);
                var checkDouble = double.TryParse(paginationParameter.Search, out validDouble);

                if (checkInt)
                {
                    filter = filter.And(x => x.BidId == validInt || x.UserId == validInt || x.FishId == validInt);
                }
                else if (checkDouble)
                {
                    filter = filter.And(x => x.Price.HasValue && Math.Abs(x.Price.Value - validDouble) < 0.01);
                }
                else
                {
                    filter = filter.And(x => x.BidCode!.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.User.UserCode!.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.User.FullName!.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.User.Mail!.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Fish.FishCode!.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Fish.FishName!.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Fish.FishType.FishTypeName!.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Fish.Auction!.AuctionName!.ToLower().Contains(paginationParameter.Search.ToLower()));
                }
            }

            if (userAuctionFilters.createDateFrom.HasValue && userAuctionFilters.createDateTo.HasValue)
            {
                if (userAuctionFilters.createDateFrom.Value > userAuctionFilters.createDateTo.Value)
                {
                    return new BusinessResult(Const.WARNING_INVALID_DATE_FILTER_CODE, Const.WARNING_INVALID_DATE_FILTER_MSG);
                }
                filter = filter.And(x => x.CreateDate >= userAuctionFilters.createDateFrom &&
                            x.CreateDate <= userAuctionFilters.createDateTo);
            }

            if (userAuctionFilters.fishTypeNames != null && userAuctionFilters.fishTypeNames.Length > 0)
            {
                filter = filter.And(x => userAuctionFilters.fishTypeNames.Contains(x.Fish.FishType.FishTypeName));
            }

            if (!string.IsNullOrEmpty(userAuctionFilters.auctionCode))
            {
                filter = filter.And(x => x.Fish.Auction!.AuctionCode == userAuctionFilters.auctionCode);
            }

            if (!string.IsNullOrEmpty(userAuctionFilters.userCode))
            {
                filter = filter.And(x => x.User.UserCode == userAuctionFilters.userCode);
            }

            if (!string.IsNullOrEmpty(userAuctionFilters.isWinner))
            {
                bool isWinner = userAuctionFilters.isWinner.ToLower() == "true";
                filter = filter.And(x => x.IsWinner == isWinner);
            }
            switch (paginationParameter.SortBy?.Trim().ToLower())
            {
                case "bidcode":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.BidCode)
                               : x => x.OrderBy(x => x.BidCode) : x => x.OrderBy(x => x.BidCode);
                    break;
                case "usercode":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.User.UserCode)
                               : x => x.OrderBy(x => x.User.UserCode) : x => x.OrderBy(x => x.User.UserCode);
                    break;
                case "fullname":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.User.FullName)
                               : x => x.OrderBy(x => x.User.FullName) : x => x.OrderBy(x => x.User.FullName);
                    break;
                case "mail":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.User.Mail)
                               : x => x.OrderBy(x => x.User.Mail) : x => x.OrderBy(x => x.User.Mail);
                    break;
                case "fishcode":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Fish.FishCode)
                               : x => x.OrderBy(x => x.Fish.FishCode) : x => x.OrderBy(x => x.Fish.FishCode);
                    break;
                case "fishname":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Fish.FishName)
                               : x => x.OrderBy(x => x.Fish.FishName) : x => x.OrderBy(x => x.Fish.FishName);
                    break;
                case "fishtypename":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Fish.FishType.FishTypeName)
                               : x => x.OrderBy(x => x.Fish.FishType.FishTypeName) : x => x.OrderBy(x => x.Fish.FishType.FishTypeName);
                    break;
                case "farmname":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Fish.Auction!.AuctionName)
                               : x => x.OrderBy(x => x.Fish.Auction!.AuctionName) : x => x.OrderBy(x => x.Fish.Auction!.AuctionName);
                    break;
                case "price":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Price)
                               : x => x.OrderBy(x => x.Price) : x => x.OrderBy(x => x.Price);
                    break;
                case "iswinner":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.IsWinner)
                               : x => x.OrderBy(x => x.IsWinner) : x => x.OrderBy(x => x.IsWinner);
                    break;
                case "createdate":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.CreateDate)
                               : x => x.OrderBy(x => x.CreateDate) : x => x.OrderBy(x => x.CreateDate);
                    break;
                default:
                    orderBy = x => x.OrderByDescending(x => x.CreateDate);
                    break;
            }
            string includeProperties = "User,Fish,Fish.FishType,Fish.Farm";
            var result = await _unitOfWork.UserAuctionRepository.Get(filter, orderBy, includeProperties, paginationParameter.PageIndex, paginationParameter.PageSize);
            var pagin = new PageEntity<UserAuctionModel>();
            pagin.List = _mapper.Map<IEnumerable<UserAuctionModel>>(result);
            pagin.TotalRecord = await _unitOfWork.UserAuctionRepository.Count(filter);
            pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, paginationParameter.PageSize);
            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, pagin);
        }

        public async Task<IBusinessResult> GetByID(string? id)
        {
            var validId = 0;
            Expression<Func<UserAuction, bool>> filter = null!;
            if (int.TryParse(id, out validId))
            {
                filter = x => x.BidId == validId;
            }
            else
            {
                return new BusinessResult(Const.WARNING_INVALID_ID_CODE, Const.WARNING_INVALID_ID_MSG);
            }
            string includeProperties = "User,Fish,Fish.FishType,Fish.Farm";
            var userAuctionEntity = await _unitOfWork.UserAuctionRepository.GetByCondition(filter, includeProperties);
            if (userAuctionEntity == null)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
            }
            var userAuctionModel = _mapper.Map<UserAuctionModel>(userAuctionEntity);
            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, userAuctionModel);
        }

        public async Task<IBusinessResult> Insert(UserAuctionModel entityInsert)
        {
            //check userid exist
            //check fishid exist
            //check iswinner if already have true in 1 fishid
            //check price must be = finalPriceCurrent + minPirceBid
            var mapEntity = _mapper.Map<UserAuction>(entityInsert);
            await _unitOfWork.UserAuctionRepository.Insert(mapEntity);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            if (result)
            {
                string includeProperties = "User,Fish,Fish.FishType,Fish.Farm";
                var userAuction = await _unitOfWork.UserAuctionRepository.GetByCondition(x => x.BidId == entityInsert.BidId, includeProperties);
                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, _mapper.Map<IEnumerable<UserAuctionModel>>(userAuction));
            }
            return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
        }

        public async Task<IBusinessResult> Update(UserAuctionModel entityUpdate)
        {
            //check userid exist
            //check fishid exist
            //check iswinner if already have true in 1 fishid
            //check price must be = finalPriceCurrent + minPirceBid
            Expression<Func<UserAuction, bool>> filter = x => x.BidId == entityUpdate.BidId;
            var entity = await _unitOfWork.UserAuctionRepository.GetByCondition(filter);
            if (entity == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            if (entityUpdate.Price.HasValue)
            {
                entity.Price = entityUpdate.Price.Value;
            }
            if (entityUpdate.IsWinner.HasValue)
            {
                entity.IsWinner = entityUpdate.IsWinner.Value;
            }
            if (entityUpdate.CreateDate.HasValue)
            {
                entity.CreateDate = entityUpdate.CreateDate.Value;
            }
            if (entityUpdate.UserId != entity.UserId)
            {
                entity.UserId = entityUpdate.UserId;
            }

            if (entityUpdate.FishId != entity.FishId)
            {
                entity.FishId = entityUpdate.FishId;
            }
            _unitOfWork.UserAuctionRepository.Update(entity);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            if (result)
            {
                string includeProperties = "User,Fish,Fish.FishType,Fish.Farm";
                var userAuction = await _unitOfWork.UserAuctionRepository.GetByCondition(x => x.BidId == entityUpdate.BidId, includeProperties);
                return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, _mapper.Map<IEnumerable<UserAuctionModel>>(userAuction));
            }
            return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
        }

        public async Task<IBusinessResult> Delete(int bidid)
        {
            var userAuction = await _unitOfWork.UserAuctionRepository.GetByCondition(x => x.BidId == bidid);
            if (userAuction == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            _unitOfWork.UserAuctionRepository.Delete(userAuction);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            if (result)
            {
                return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, true);
            }
            return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
        }

    }
}
