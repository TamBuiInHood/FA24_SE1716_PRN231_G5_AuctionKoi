﻿using AutoMapper;
using KoiAuction.BussinessModels.Pagination;
using KoiAuction.BussinessModels.Proposal;
using KoiAuction.Common;
using KoiAuction.Service.Base;
using KoiAuction.Service.ISerivice;
using Microsoft.AspNetCore.Routing.Tree;
using Microsoft.IdentityModel.Tokens;
using PRN231.AuctionKoi.Common.Utils;
using PRN231.AuctionKoi.Common.Utils.Common.Enums;
using PRN231.AuctionKoi.Repository.Entities;
using PRN231.AuctionKoi.Repository.UnitOfWork;
using System.Linq.Expressions;
using KoiAuction.Repository.Entities;
using KoiAuction.API.Payloads.Requests.Filters;

namespace KoiAuction.Service.Services
{
    public class ProposalService : IProposalService
    {
        public IUnitOfWork _unitOfWork;
        public IMapper _mappper;

        public ProposalService(IUnitOfWork unitOfWork, IMapper mappper)
        {
            _unitOfWork = unitOfWork;
            _mappper = mappper;
        }

        public async Task<IBusinessResult> Delete(int id)
        {
            try
            {
                var checkExist = await _unitOfWork.ProposalRepository.GetByID(id);
                if (checkExist != null)
                {
                    var result = await _unitOfWork.ProposalRepository.DeleteProposal(id);
                    if (result)
                    {
                        return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, result);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG, result);
                    }
                }
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, "Can not find any proposal with that id");
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, Const.FAIL_DELETE_MSG, ex.Message.ToString());
            }
           
        }

        public async Task<IBusinessResult> Get(PaginationParameter paginationParameter)
        {
            try
            {
                Expression<Func<Proposal, bool>> filter = null!;
                Func<IQueryable<Proposal>, IOrderedQueryable<Proposal>> orderBy = null!;
                if (!(paginationParameter.Search == null || paginationParameter.Search.Equals("")))
                {
                    int validInt = 0;
                    var checkInt = int.TryParse(paginationParameter.Search, out validInt);
                    if (checkInt)
                    {
                        filter = x => x.FarmId == validInt;
                    }
                    else
                    {
                        filter = x => x.FarmCode.ToLower().Contains(paginationParameter.Search.ToLower())
                                      || x.FarmName.ToLower().Contains(paginationParameter.Search.ToLower())
                                      || x.Location.ToLower().Contains(paginationParameter.Search.ToLower())
                                      || x.Status.ToLower().Contains(paginationParameter.Search.ToLower())
                                      || x.Description.ToLower().Contains(paginationParameter.Search.ToLower())
                                      || x.Owner.ToLower().Contains(paginationParameter.Search.ToLower())
                                      || x.User.FullName.ToLower().Contains(paginationParameter.Search.ToLower());
                    }
                }
                switch (paginationParameter.SortBy)
                {
                    case "farmid":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.FarmId)
                                   : x => x.OrderBy(x => x.FarmId) : x => x.OrderBy(x => x.FarmId);
                        break;
                    case "farmcode":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.FarmCode)
                                   : x => x.OrderBy(x => x.FarmCode) : x => x.OrderBy(x => x.FarmCode);
                        break;
                    case "farmname":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.FarmName)
                                   : x => x.OrderBy(x => x.FarmName) : x => x.OrderBy(x => x.FarmName);
                        break;
                    case "location":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.Location)
                                   : x => x.OrderBy(x => x.Location) : x => x.OrderBy(x => x.Location);
                        break;
                    case "status":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.Status)
                                   : x => x.OrderBy(x => x.Status) : x => x.OrderBy(x => x.Status);
                        break;
                    case "description":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.Description)
                                   : x => x.OrderBy(x => x.Description) : x => x.OrderBy(x => x.Description);
                        break;
                    case "owner":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.Owner)
                                   : x => x.OrderBy(x => x.Owner) : x => x.OrderBy(x => x.Owner);
                        break;
                    case "fullname":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.User.FullName)
                                   : x => x.OrderBy(x => x.User.FullName) : x => x.OrderBy(x => x.User.FullName);
                        break;
                    case "createdate":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.CreateDate)
                                   : x => x.OrderBy(x => x.CreateDate) : x => x.OrderBy(x => x.CreateDate);
                        break;
                    default:
                        orderBy = x => x.OrderBy(x => x.FarmId);
                        break;
                }
                string includeProperties = "User,DetailProposals";
                var result = await _unitOfWork.ProposalRepository.Get(filter, orderBy, includeProperties, paginationParameter.PageIndex, paginationParameter.PageSize);
                var pagin = new PageEntity<ProposalModel>();
                pagin.List = _mappper.Map<IEnumerable<ProposalModel>>(result);
                pagin.TotalRecord = await _unitOfWork.ProposalRepository.Count();
                pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, paginationParameter.PageSize);
                if (pagin.List.Any())
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, pagin);
                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<ProposalModel>());
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, Const.FAIL_READ_MSG, ex.ToString());
            }

        }

        public async Task<IBusinessResult> GetByID(int id)
        {
            try
            {
                string includeProperties = "User,DetailProposals";
                var result = await _unitOfWork.ProposalRepository.GetByCondition(x => x.FarmId == id, includeProperties: includeProperties);
                if (result != null)
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);

                }
                else
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new Proposal());
                }
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, Const.FAIL_READ_MSG, ex.ToString());
            }
        }

        public async Task<IBusinessResult> Insert(CreateProposalModel entityinsert)
        {
            try
            {
                var data = _mappper.Map<Proposal>(entityinsert);
                await _unitOfWork.ProposalRepository.Insert(data);
                var result = await _unitOfWork.SaveAsync();
                if(result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, result);

                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG, result);
                }
            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, Const.FAIL_CREATE_MSG, ex.Message.ToString());
            }
        }

        public async Task<IBusinessResult> Update(int id, UpdateProposalModel entityUpdate)
        {
            try
            {
                var checkExist = await _unitOfWork.ProposalRepository.GetByID(id);
                if(checkExist != null)
                {

                    checkExist.FarmName = entityUpdate.FarmName;
                    checkExist.Location = entityUpdate.Location;
                    checkExist.AvatarUrl = entityUpdate.AvatarUrl;
                    checkExist.CreateDate = entityUpdate.CreateDate;
                    checkExist.Status = entityUpdate.Status;
                    checkExist.Description = entityUpdate.Description;
                    checkExist.Owner = entityUpdate.Owner;
                    checkExist.UpdateDate = entityUpdate.UpdateDate;
                    checkExist.UserId = entityUpdate.UserId;

                    var result = await _unitOfWork.SaveAsync();
                    if(result > 0)
                    {
                        return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, result);
                    }
                    else
                    {
                        return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, result);
                    }
                }

                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, "Can not find any proposal to update with that id");

            }
            catch (Exception ex)
            {

                return new BusinessResult(Const.ERROR_EXCEPTION, Const.FAIL_UPDATE_MSG, ex.Message.ToString());
            }
        }
    }
}
