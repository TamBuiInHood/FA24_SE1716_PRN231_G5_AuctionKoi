using AutoMapper;
using KoiAuction.BussinessModels.DetailProposalModel;
using KoiAuction.BussinessModels.Filters;
using KoiAuction.BussinessModels.Pagination;
using KoiAuction.BussinessModels.PaymentModels;
using KoiAuction.BussinessModels.Proposal;
using KoiAuction.BussinessModels.UserAuctionModels;
using KoiAuction.BussinessModels.UserModels;
using KoiAuction.Common;
using KoiAuction.Common.Enums;
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
using System.ComponentModel.DataAnnotations;
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
                                  || x.Fish.Auction!.AuctionCode!.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Fish.Auction!.AuctionName!.ToLower().Contains(paginationParameter.Search.ToLower()));
                }
            }

            if (userAuctionFilters.priceFrom.HasValue || userAuctionFilters.priceTo.HasValue)
            {
                if (!userAuctionFilters.priceFrom.HasValue || !userAuctionFilters.priceTo.HasValue)
                {
                    return new BusinessResult(Const.WARNING_MISSING_PRICE_FILTER_CODE, Const.WARNING_MISSING_PRICE_FILTER_MSG);
                }

                if (userAuctionFilters.priceFrom.Value > userAuctionFilters.priceTo.Value)
                {
                    return new BusinessResult(Const.FAIL_CHECK_NUMBER_FILTER_CODE, Const.FAIL_CHECK_NUMBER_FILTER_MSG);
                }
                filter = filter.And(x => x.Price <= userAuctionFilters.priceTo &&
                                         x.Price >= userAuctionFilters.priceFrom);
            }

            if (userAuctionFilters.createDateFrom.HasValue || userAuctionFilters.createDateTo.HasValue)
            {
                if (!userAuctionFilters.createDateFrom.HasValue || !userAuctionFilters.createDateTo.HasValue)
                {
                    return new BusinessResult(Const.WARNING_MISSING_DATE_FILTER_CODE, Const.WARNING_MISSING_DATE_FILTER_MSG);
                }

                if (userAuctionFilters.createDateFrom.Value > userAuctionFilters.createDateTo.Value)
                {
                    return new BusinessResult(Const.WARNING_INVALID_DATE_FILTER_CODE, Const.WARNING_INVALID_DATE_FILTER_MSG);
                }
                filter = filter.And(x => x.CreateDate >= userAuctionFilters.createDateFrom &&
                                         x.CreateDate <= userAuctionFilters.createDateTo);
            }

            if (userAuctionFilters.isWinner != null)
                filter = filter.And(x => x.IsWinner.ToString()!.ToLower() == userAuctionFilters.isWinner.ToLower());

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
                case "auctioncode":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Fish.Auction!.AuctionCode)
                               : x => x.OrderBy(x => x.Fish.Auction!.AuctionCode) : x => x.OrderBy(x => x.Fish.Auction!.AuctionCode);
                    break;
                case "farmname":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Fish.Farm.FarmName)
                               : x => x.OrderBy(x => x.Fish.Farm.FarmName) : x => x.OrderBy(x => x.Fish.Farm.FarmName);
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
            string includeProperties = "User,Fish,Fish.FishType,Fish.Farm,Fish.Auction";
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
            string includeProperties = "User,Fish,Fish.FishType,Fish.Farm,Fish.Auction";
            var userAuctionEntity = await _unitOfWork.UserAuctionRepository.GetByCondition(filter, includeProperties);
            if (userAuctionEntity == null)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
            }
            var userAuctionModel = _mapper.Map<UserAuctionModel>(userAuctionEntity);
            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, userAuctionModel);
        }

        public async Task<IBusinessResult> GetByAuctionIdAndFishId(string? auctionId, string? fishId)
        {
            var validFishId = 0;
            var validAuctionId = 0;
            Expression<Func<UserAuction, bool>> filter = null!;
            if (int.TryParse(fishId, out validFishId) && int.TryParse(auctionId, out validAuctionId))
            {
                filter = x => x.Fish.FishId == validFishId && x.Fish.Auction!.AuctionId == validAuctionId;
            }
            else
            {
                return new BusinessResult(Const.WARNING_INVALID_ID_CODE, Const.WARNING_INVALID_ID_MSG);
            }
            Func<IQueryable<UserAuction>, IOrderedQueryable<UserAuction>> orderBy = null!;
            string includeProperties = "User,Fish";
            orderBy = x => x.OrderByDescending(x => x.CreateDate)
                            .ThenByDescending(x => x.BidId);
            var userAuctionListEntity = await _unitOfWork.UserAuctionRepository.GetAllNoPaging(filter, orderBy, includeProperties);
            if (userAuctionListEntity == null || !userAuctionListEntity.Any())
            {
                return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
            }
            var userAuctionModel = _mapper.Map<UserAuctionModel[]>(userAuctionListEntity);
            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, userAuctionModel);
        }
        public async Task<IBusinessResult> GetListUser()
        {
            try
            {
                var listUser = await _unitOfWork.UserRepository.GetAllNoPaging(x => x.RoleId == (int)UserRole.AUCTIONER);
                if (listUser != null && listUser.Any())
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, _mapper.Map<UserModel[]>(listUser));
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, Const.FAIL_READ_MSG, ex.ToString());
            }
        }

        public async Task<IBusinessResult> GetListDetailProposal()
        {
            try
            {
                var listUser = await _unitOfWork.DetailProposalRepository.GetAllNoPaging();
                if (listUser != null && listUser.Any())
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, _mapper.Map<DetailProposalModel[]>(listUser));
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, Const.FAIL_READ_MSG, ex.ToString());
            }
        }

        public async Task<IBusinessResult> GetListAuction()
        {
            try
            {
                var listAuctions = await _unitOfWork.DetailProposalRepository.ListAuctions();
                if (listAuctions != null && listAuctions.Any())
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, listAuctions);
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, Const.FAIL_READ_MSG, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Insert(CreateUserAuctionModel entityInsert)
        {
            var validationResult = await ValidateAuctionConditions(entityInsert);
            if (validationResult.Status != Const.SUCCESS_CHECK_CODE)
            {
                return validationResult;
            }

            var mapEntity = _mapper.Map<UserAuction>(entityInsert);
            await _unitOfWork.UserAuctionRepository.Insert(mapEntity);
            var detailProposal = await _unitOfWork.DetailProposalRepository.GetByCondition(x => x.FishId == entityInsert.FishId && x.AuctionId == entityInsert.AuctionId);
            detailProposal.FinalPrice = entityInsert.Price;
            _unitOfWork.DetailProposalRepository.Update(detailProposal);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            if (result)
            {
                string includeUserAuctionProperties = "User,Fish,Fish.FishType,Fish.Farm,Fish.Auction";
                var userAuction = await _unitOfWork.UserAuctionRepository.GetByCondition(x => x.BidCode == entityInsert.BidCode, includeUserAuctionProperties);
                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, _mapper.Map<UserAuctionModel>(userAuction));
            }
            return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
        }

        public async Task<IBusinessResult> Update(int bidId, UpdateUserAuctionModel entityUpdate)
        {
            var detailProposalResult = await ValidateDetailProposal(entityUpdate.FishId, entityUpdate.AuctionId, entityUpdate.Price!.Value, true);
            if (!detailProposalResult.IsSuccess)
            {
                return detailProposalResult.BusinessResult;
            }

            // check iswinner if already have true in 1 auction
            if (entityUpdate.IsWinner == true)
            {
                var isExistWinner = await _unitOfWork.UserAuctionRepository.GetByCondition(x => x.IsWinner == true &&
                                                                                x.FishId == entityUpdate.FishId &&
                                                                                x.Fish.Auction!.AuctionId == entityUpdate.AuctionId &&
                                                                                x.UserId != entityUpdate.UserId);
                if (isExistWinner != null)
                {
                    return new BusinessResult(Const.WARNING_EXIST_WINNER_CODE, Const.WARNING_EXIST_WINNER_MSG);
                }
            }

            var entity = await _unitOfWork.UserAuctionRepository.GetByCondition(x => x.BidId == bidId);
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
            //if (entityUpdate.UserId != entity.UserId)
            //{
            //    entity.UserId = entityUpdate.UserId;
            //}

            //if (entityUpdate.FishId != entity.FishId)
            //{
            //    entity.FishId = entityUpdate.FishId;
            //}
            _unitOfWork.UserAuctionRepository.Update(entity);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            if (result)
            {
                string includePropertiesUserAuction = "User,Fish,Fish.FishType,Fish.Farm,Fish.Auction";
                var userAuction = await _unitOfWork.UserAuctionRepository.GetByCondition(x => x.BidId == bidId, includePropertiesUserAuction);
                return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, _mapper.Map<UserAuctionModel>(userAuction));
            }
            return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
        }

        public async Task<IBusinessResult> Delete(int bidid)
        {
            var userAuction = await _unitOfWork.UserAuctionRepository.GetByID(bidid);
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

        private async Task<IBusinessResult> ValidateAuctionConditions(CreateUserAuctionModel entity)
        {
            //check userid exist
            var userEntity = await _unitOfWork.UserRepository.GetByID(entity.UserId);
            if (userEntity == null)
            {
                return new BusinessResult(Const.WARNING_INVALID_USER_ID_CODE, Const.WARNING_INVALID_USER_ID_MSG);
            }

            var detailProposalResult = await ValidateDetailProposal(entity.FishId, entity.AuctionId, entity.Price!.Value, false);
            if (!detailProposalResult.IsSuccess)
            {
                return detailProposalResult.BusinessResult;
            }

            // check iswinner if already have true in 1 auction
            if (entity.IsWinner == true)
            {
                var userAuctionEntity = await _unitOfWork.UserAuctionRepository.GetByCondition(x => x.IsWinner == true &&
                                                                                x.FishId == entity.FishId &&
                                                                                x.Fish.Auction!.AuctionId == entity.AuctionId);
                if (userAuctionEntity != null)
                {
                    return new BusinessResult(Const.WARNING_EXIST_WINNER_CODE, Const.WARNING_EXIST_WINNER_MSG);
                }
            }

            // Nếu không có lỗi trả về kết quả thành công
            return new BusinessResult(Const.SUCCESS_CHECK_CODE, Const.SUCCESS_CHECK_MSG);
        }

        private async Task<(bool IsSuccess, IBusinessResult BusinessResult)> ValidateDetailProposal(int fishId, int auctionId, double price, Boolean isEdit)
        {
            // check fishId and auctionId exist
            Expression<Func<DetailProposal, bool>> filter = null!;
            filter = x => x.FishId == fishId && x.AuctionId == auctionId;
            string includeProperties = "Auction";
            var detailProposalEntity = await _unitOfWork.DetailProposalRepository.GetByCondition(filter, includeProperties);
            if (detailProposalEntity == null)
            {
                return (false, new BusinessResult(Const.WARNING_INVALID_USER_AUCTION_CODE, Const.WARNING_INVALID_USER_AUCTION_MSG));
            }

            // check auction and detailProposal is still active
            if (!isEdit && detailProposalEntity.Auction!.Status != AuctionStatus.Active.ToString() && detailProposalEntity.Status != DetailProposalStatus.Active.ToString())
            {
                return (false, new BusinessResult(Const.WARNING_AUCTION_IN_ACTIVE_CODE, Const.WARNING_AUCTION_IN_ACTIVE_MSG));
            }

            // check price must be >= finalPriceCurrent + minPirceBid
            var minPrice = detailProposalEntity.FinalPrice + detailProposalEntity.MinIncrement;
            if (price < minPrice)
            {
                return (false, new BusinessResult(Const.WARNING_INVALID_AUCTION_PRICE_CODE, String.Format(Const.WARNING_INVALID_AUCTION_PRICE_MSG, minPrice)));
            }

            return (true, null!);
        }

    }
}
