using AutoMapper;
using KoiAuction.BussinessModels.CheckingProposal;
using KoiAuction.BussinessModels.Order;
using KoiAuction.BussinessModels.Pagination;
using KoiAuction.Common;
using KoiAuction.Repository.Entities;
using KoiAuction.Service.Base;
using KoiAuction.Service.ISerivice;
using PRN231.AuctionKoi.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Service.Services;

public class CheckingProposalService : ICheckingProposalService
{
    public IUnitOfWork _unitOfWork;
    public IMapper _mappper;

    public CheckingProposalService(IUnitOfWork unitOfWork, IMapper mappper)
    {
        _unitOfWork = unitOfWork;
        _mappper = mappper;
    }

    public async Task<IBusinessResult> Delete(int id)
    {
        var checkingPropo = await _unitOfWork.CheckingProposalRepository.GetByID(id);
        if (checkingPropo == null)
        {
            return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
        }
        _unitOfWork.CheckingProposalRepository.Delete(checkingPropo);
        var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
        if (result)
        {
            return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, true);
        }
        return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
    }

    /*public async Task<IBusinessResult> Get(PaginationParameter paginationParameter)
    {
        try
        {
            Expression<Func<CheckingProposal, bool>> filter = null!;
            Func<IQueryable<CheckingProposal>, IOrderedQueryable<CheckingProposal>> orderBy = null!;

            if (!string.IsNullOrEmpty(paginationParameter.Search))
            {
                // Initialize filter based on search criteria
                int validInt = 0;
                int validFishId = 0;
                DateTime validDate;

                if (int.TryParse(paginationParameter.Search, out validInt))
                {
                    filter = x => x.CheckingProposalId == validInt;
                }
                else if (int.TryParse(paginationParameter.Search, out validFishId))
                {
                    filter = x => x.FishId == validFishId;
                }
                else if (DateTime.TryParse(paginationParameter.Search, out validDate))
                {
                    filter = x => x.SubmissionDate == validDate
                                || x.CheckingDate == validDate
                                || x.ExpiredDate == validDate;
                }
                else
                {
                    string searchLowered = paginationParameter.Search.ToLower();
                    filter = x => x.CheckingProposalCode.ToLower().Contains(searchLowered)
                               || x.Status.ToLower().Contains(searchLowered)
                               || x.TermAndCodition.ToLower().Contains(searchLowered)
                               || x.Attachment.ToLower().Contains(searchLowered)
                               || x.Note.ToLower().Contains(searchLowered);
                }
            }

            // Sorting logic
            switch (paginationParameter.SortBy?.ToLower())
            {
                case "checkingproposalid":
                    orderBy = paginationParameter.Direction?.ToLower() == "desc"
                              ? x => x.OrderByDescending(x => x.CheckingProposalId)
                              : x => x.OrderBy(x => x.CheckingProposalId);
                    break;

                case "checkingproposalcode":
                    orderBy = paginationParameter.Direction?.ToLower() == "desc"
                              ? x => x.OrderByDescending(x => x.CheckingProposalCode)
                              : x => x.OrderBy(x => x.CheckingProposalCode);
                    break;

                case "status":
                    orderBy = paginationParameter.Direction?.ToLower() == "desc"
                              ? x => x.OrderByDescending(x => x.Status)
                              : x => x.OrderBy(x => x.Status);
                    break;

                case "termandcoditon":
                    orderBy = paginationParameter.Direction?.ToLower() == "desc"
                              ? x => x.OrderByDescending(x => x.TermAndCodition)
                              : x => x.OrderBy(x => x.TermAndCodition);
                    break;

                case "submissiondate":
                    orderBy = paginationParameter.Direction?.ToLower() == "desc"
                              ? x => x.OrderByDescending(x => x.SubmissionDate)
                              : x => x.OrderBy(x => x.SubmissionDate);
                    break;

                case "checkingdate":
                    orderBy = paginationParameter.Direction?.ToLower() == "desc"
                              ? x => x.OrderByDescending(x => x.CheckingDate)
                              : x => x.OrderBy(x => x.CheckingDate);
                    break;

                case "expireddate":
                    orderBy = paginationParameter.Direction?.ToLower() == "desc"
                              ? x => x.OrderByDescending(x => x.ExpiredDate)
                              : x => x.OrderBy(x => x.ExpiredDate);
                    break;

                default:
                    orderBy = x => x.OrderBy(x => x.CheckingProposalId);
                    break;
            }

            string includeProperties = "Fish";
            var result = await _unitOfWork.CheckingProposalRepository.Get(filter, orderBy, includeProperties, paginationParameter.PageIndex, paginationParameter.PageSize);

            var pagin = new PageEntity<CheckingProposalModel>();
            pagin.List = _mappper.Map<IEnumerable<CheckingProposalModel>>(result);
            pagin.TotalRecord = await _unitOfWork.ProposalRepository.Count();
            pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, paginationParameter.PageSize);


            if (pagin.List.Any())
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, pagin);
            }
            else
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<CheckingProposalModel>());
            }
        }
        catch (Exception ex)
        {
            // Log exception here using your logging framework
            return new BusinessResult(Const.ERROR_EXCEPTION, Const.FAIL_READ_MSG, ex.ToString());
        }
    }*/

    public async Task<IBusinessResult> Get(string? searchKey, string? orderBy, int? pageIndex = null, int? pageSize = null)
    {
        try
        {
            var checkingProposals = await _unitOfWork.CheckingProposalRepository.Get(includeProperties: "Fish");

            if (checkingProposals == null || !checkingProposals.Any())
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }


            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                checkingProposals = checkingProposals.Where(o =>
                    (o.CheckingProposalCode != null && o.CheckingProposalCode.Contains(searchKey)) ||
                    (o.Status != null && o.Status.Contains(searchKey)));
            }

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                checkingProposals = orderBy.ToLower() switch
                {
                    "Checkingcode" => checkingProposals.OrderBy(o => o.CheckingProposalCode),
                    "Status" => checkingProposals.OrderBy(o => o.Status),
                    _ => checkingProposals
                };
            }


            int currentPageIndex = (pageIndex ?? 1) - 1;
            var items = checkingProposals.Skip(currentPageIndex * (pageSize ?? 10))
                              .Take(pageSize ?? 10)
                              .ToList();

            var orderDtos = _mappper.Map<List<CheckingProposalModel>>(items);
            var totalRecords = checkingProposals.Count();

            var pageEntity = new PageEntity<CheckingProposalModel>
            {
                List = orderDtos,
                TotalPage = (int)Math.Ceiling((double)totalRecords / (pageSize ?? 10)),
                TotalRecord = totalRecords
            };

            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, pageEntity);
        }
        catch (Exception ex)
        {
            return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
        }
    }

        public async Task<IBusinessResult> GetByID(int id)
    {
        var checkingProposal = await _unitOfWork.CheckingProposalRepository.GetCheckingByID(id);

        return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG, checkingProposal);
    }

    public async Task<IBusinessResult> GetFish()
    {
        try
        {
            var listFish = await _unitOfWork.DetailProposalRepository.GetAllNoPaging();
            if (listFish != null && listFish.Any())
            {
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, listFish);
            }
            else
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<DetailProposal>());
            }
        }
        catch (Exception ex)
        {

            return new BusinessResult(Const.ERROR_EXCEPTION, Const.FAIL_READ_MSG, ex.ToString());
        }
    }

    public async Task<IBusinessResult> Insert(CreateCheckingProposalModel entityinsert)
    {
        var data = _mappper.Map<CheckingProposal>(entityinsert);
        await _unitOfWork.CheckingProposalRepository.Insert(data);
        var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
        if (result)
        {
            return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
        }
        return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
    }

    /*public async Task<IBusinessResult> Test(UpdateCheckingProposalModel entityUpdate)
    {
        try
        {
            var listcheckingProposal = await _unitOfWork.CheckingProposalRepository.GetAllNoPaging();
            if (listcheckingProposal == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, new List<CheckingProposal>());
            }
            else
            {
                return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new List<CheckingProposal>());

            }
        }
        catch (Exception ex)
        {

            return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
        }
    }*/

    public async Task<IBusinessResult> Update(int id, UpdateCheckingProposalModel entityUpdate)
    {

        try
        {
            var checkExist = await _unitOfWork.CheckingProposalRepository.GetByID(id);
            if (checkExist != null)
            {
                if (!string.IsNullOrEmpty(entityUpdate.CheckingProposalCode))
                {
                    checkExist.CheckingProposalCode = entityUpdate.CheckingProposalCode;
                }
                if (!string.IsNullOrEmpty(entityUpdate.ImageUrl))
                {
                    checkExist.ImageUrl = entityUpdate.ImageUrl;
                }
                if (entityUpdate.SubmissionDate.HasValue)
                {
                    checkExist.SubmissionDate = entityUpdate.SubmissionDate;
                }
                if (entityUpdate.CheckingDate.HasValue)
                {
                    checkExist.CheckingDate = entityUpdate.CheckingDate;
                }
                if (entityUpdate.ExpiredDate.HasValue)
                {
                    checkExist.ExpiredDate = entityUpdate.ExpiredDate;
                }
                if (!string.IsNullOrEmpty(entityUpdate.Note))
                {
                    checkExist.Note = entityUpdate.Note;
                }
                if (!string.IsNullOrEmpty(entityUpdate.TermAndCodition))
                {
                    checkExist.ImageUrl = entityUpdate.TermAndCodition;
                }
                if (!string.IsNullOrEmpty(entityUpdate.Attachment))
                {
                    checkExist.Attachment = entityUpdate.Attachment;
                }
                if (!string.IsNullOrEmpty(entityUpdate.Status))
                {
                    checkExist.Status = entityUpdate.Status;
                }
                if (checkExist.FishId.HasValue)
                {
                    checkExist.FishId = entityUpdate.FishId;
                }
                if (checkExist.AuctionFee.HasValue)
                {
                    checkExist.AuctionFee = entityUpdate.AuctionFee;
                }

                _unitOfWork.CheckingProposalRepository.Update(checkExist);
                var result = await _unitOfWork.SaveAsync();
                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, result);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, result);
                }
            }

            return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG, "Can not find any checking proposal to update with that id");

        }
        catch (Exception ex)
        {

            return new BusinessResult(Const.ERROR_EXCEPTION, Const.FAIL_UPDATE_MSG, ex.Message.ToString());
        }
    }
}
