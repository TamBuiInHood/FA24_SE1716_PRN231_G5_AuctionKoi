using AutoMapper;
using KoiAuction.Common;
using KoiAuction.Common.Constants;
using KoiAuction.Service.Base;
using KoiAuction.Service.ISerivice;
using KoiAuction.BussinessModels.Pagination;
using Microsoft.IdentityModel.Tokens;
using PRN231.AuctionKoi.Common.Utils;
using PRN231.AuctionKoi.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KoiAuction.BussinessModels.PaymentModels;
using KoiAuction.Repository.Entities;
using KoiAuction.BussinessModels.Filters;
using KoiAuction.Common.Utils;


namespace KoiAuction.Service.Services
{
    public class PaymnetService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymnetService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IBusinessResult> Delete(int id)
        {
            var paymnet = await _unitOfWork.PaymentRepository.GetByCondition(x => x.PaymentId == id);
            if (paymnet == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            _unitOfWork.PaymentRepository.Delete(paymnet);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG, true);
        }

        public async Task<IBusinessResult> Get(PaginationParameter paginationParameter, PaymentFilters paymentFilter)
        {
            try
            {
                Expression<Func<Payment, bool>> filter = null!;
                Func<IQueryable<Payment>, IOrderedQueryable<Payment>> orderBy = null!;

                if (!(paginationParameter.Search == null || paginationParameter.Search.Equals("")))
                {
                    int validInt = 0;
                    double validDouble = 0;

                    //var checkInt = int.TryParse(paginationParameter.Search, out validInt);
                    //var checkDouble = double.TryParse(paginationParameter.Search, out validDouble);

                    if (int.TryParse(paginationParameter.Search, out validInt))
                    {
                        filter = filter.And(x => x.PaymentId == validInt || x.OrderId == validInt || x.TransactionId == validInt);
                    }
                    else if (double.TryParse(paginationParameter.Search, out validDouble))
                    {
                        filter = filter.And(x => x.PaymentAmount.HasValue && Math.Abs(x.PaymentAmount.Value - validDouble) < 0.01);
                    }
                    else
                    {
                        filter = filter.And(x => x.Order.OrderCode!.ToLower().Contains(paginationParameter.Search.ToLower())
                                      || x.Order.ShippingMethod!.ToLower().Contains(paginationParameter.Search.ToLower()));
                                      //|| x.TransactionId!.ToLower().Contains(paginationParameter.Search.ToLower())
                    }
                }

                if (paymentFilter.createDateFrom.HasValue && paymentFilter.createDateTo.HasValue)
                {
                    if (paymentFilter.createDateFrom.Value > paymentFilter.createDateTo.Value)
                    {
                        return new BusinessResult(Const.FAIL_CHECK_DATE_FILTER_CODE, Const.FAIL_CHECK_DATE_FILTER_MSG);
                    }
                    filter = filter.And(x => x.PaymentDate >= paymentFilter.createDateFrom &&
                                x.PaymentDate <= paymentFilter.createDateTo);
                }

                if (paymentFilter.PaymentAmountFrom.HasValue && paymentFilter.PaymentAmountTo.HasValue)
                {
                    if (paymentFilter.PaymentAmountFrom.Value > paymentFilter.PaymentAmountTo.Value)
                    {
                        return new BusinessResult(Const.FAIL_CHECK_NUMBER_FILTER_CODE, Const.FAIL_CHECK_NUMBER_FILTER_MSG);
                    }
                    filter = filter.And(x => x.PaymentAmount >= paymentFilter.PaymentAmountTo &&
                                x.PaymentAmount <= paymentFilter.PaymentAmountFrom);
                }
                if (!string.IsNullOrEmpty(paymentFilter.Status))
                {
                    filter = filter.And(x => x.Status!.ToLower().Contains(paymentFilter.Status.ToLower()));
                }

                switch (paginationParameter.SortBy?.Trim().ToLower())
                {
                    case "OrderId":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.OrderId)
                                   : x => x.OrderBy(x => x.OrderId) : x => x.OrderBy(x => x.OrderId);
                        break;

                    case "PaymentAmount":

                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.PaymentAmount)
                               : x => x.OrderBy(x => x.PaymentAmount) : x => x.OrderBy(x => x.PaymentAmount);
                        break;

                    case "PaymentDate":

                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.PaymentDate)
                               : x => x.OrderBy(x => x.PaymentDate) : x => x.OrderBy(x => x.PaymentDate);
                        break;
                    case "Status":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Status)
                               : x => x.OrderBy(x => x.Status) : x => x.OrderBy(x => x.Status);
                        break;

                    case "TransactionId":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.TransactionId)
                               : x => x.OrderBy(x => x.TransactionId) : x => x.OrderBy(x => x.TransactionId);
                        break;
                    case "OrderCode":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Order.OrderCode)
                               : x => x.OrderBy(x => x.Order.OrderCode) : x => x.OrderBy(x => x.Order.OrderCode);
                        break;
                    default:
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.PaymentId)
                               : x => x.OrderBy(x => x.PaymentId) : x => x.OrderBy(x => x.PaymentId);
                        break;
                }

                string includeProperties = "Order";
                var result = await _unitOfWork.PaymentRepository.Get(filter!, orderBy, includeProperties, paginationParameter.PageIndex, paginationParameter.PageSize);
                var pagin = new PageEntity<PaymentModel>();
                pagin.List = _mapper.Map<IEnumerable<PaymentModel>>(result);

                pagin.TotalRecord = await _unitOfWork.PaymentRepository.Count(filter);
                pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, paginationParameter.PageSize);
                return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, pagin);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, ex.ToString());
            }
        }

        public async Task<IBusinessResult> GetByID(string? idKey)
        {
            var id = 0;
            Expression<Func<Payment, bool>> filter = null!;
            if (int.TryParse(idKey, out id))
            {
                filter = x => x.PaymentId == id || x.OrderId == id;
            }
            else
            {
                //filter = x => x.TransactionId == id;
            }
            string includeProperties = "Order";
            var payment = await _unitOfWork.PaymentRepository.GetByCondition(filter, includeProperties);
            if (payment == null)
            {
                return new BusinessResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
            }
            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, payment);

        }

        public async Task<IBusinessResult> Insert(PaymentModel entityInsert)
        {
            entityInsert.PaymentDate = DateTime.Now;
            var mapEntity = _mapper.Map<Payment>(entityInsert);
            await _unitOfWork.PaymentRepository.Insert(mapEntity);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            if (result)
            {
                return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
            }
            return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
        }

        public async Task<IBusinessResult> Update(PaymentModel entityUpdate)
        {
            Expression<Func<Payment, bool>> filter = x => x.PaymentId == entityUpdate.PaymentId
                                                            || x.OrderId == entityUpdate.OrderId
                                                            || x.TransactionId == entityUpdate.TransactionId;
            var entity = await _unitOfWork.PaymentRepository.GetByCondition(filter);
            if (entity == null)
            {
                return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
            }
            if (entityUpdate.PaymentAmount.HasValue)
            {
                entity.PaymentAmount = entityUpdate.PaymentAmount.Value;
            }
            if (!entityUpdate.Status.IsNullOrEmpty())
            {
                entity.Status = entityUpdate.Status;
            }
            if (entityUpdate.PaymentDate.HasValue)
            {
                entity.PaymentDate = entityUpdate.PaymentDate.Value;
            }
            if (!entityUpdate.PaymentMethod.IsNullOrEmpty())
            {
                entity.PaymentMethod = entityUpdate.PaymentMethod;
            }
            _unitOfWork.PaymentRepository.Update(entity);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            if (result)
            {
                return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
            }
            return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
        }
    }
}
