using AutoMapper;
using KoiAuction.BussinessModels.Order;
using KoiAuction.BussinessModels.Pagination;
using KoiAuction.Common;
using KoiAuction.Common.Enums;
using KoiAuction.Repository.Entities;
using KoiAuction.Service.Base;
using KoiAuction.Service.ISerivice;
using PRN231.AuctionKoi.Common.Utils;
using PRN231.AuctionKoi.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Service.Services
{
    public class OrderService : IOrderService
    {
        public IUnitOfWork _unitOfWork;
        public IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mappper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mappper;
        }
        public async Task<IBusinessResult> GetUser()
        {
            var User = await _unitOfWork.UserRepository.Get();

            if (User == null)
            {
                return new BusinessResult(Const.FAIL_DELETE_CODE, "User not found.");
            }
            return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG,User);
        }
        public async Task<IBusinessResult> Delete(int id)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByID(id);

                if (order == null)
                {
                    return new BusinessResult(Const.FAIL_DELETE_CODE, "Order not found.");
                }

                _unitOfWork.OrderRepository.Delete(order);

                var result = await _unitOfWork.SaveAsync() > 0;

                if (result)
                {
                    return new BusinessResult(Const.SUCCESS_DELETE_CODE, Const.SUCCESS_DELETE_MSG);
                }

                return new BusinessResult(Const.FAIL_DELETE_CODE, Const.FAIL_DELETE_MSG);
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
            
        }

        public async Task<IBusinessResult> Get(string? searchKey, string? orderBy, int? pageIndex = null, int? pageSize = null)
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.Get(includeProperties: "User");

                if (orders == null || !orders.Any())
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }

               
                if (!string.IsNullOrWhiteSpace(searchKey))
                {
                    orders = orders.Where(o =>
                        (o.OrderCode != null && o.OrderCode.Contains(searchKey)) ||
                        (o.TaxCode != null && o.TaxCode.Contains(searchKey)) ||
                        (int.TryParse(searchKey, out int statusKey) && o.Status == statusKey));
                }

                if (!string.IsNullOrWhiteSpace(orderBy))
                {
                    orders = orderBy.ToLower() switch
                    {
                        "ordercode" => orders.OrderBy(o => o.OrderCode),
                        "orderdate" => orders.OrderBy(o => o.OrderDate),
                        _ => orders
                    };
                }


                int currentPageIndex = (pageIndex ?? 1) - 1; 
                var items = orders.Skip(currentPageIndex * (pageSize ?? 10))
                                  .Take(pageSize ?? 10)
                                  .ToList();

                var orderDtos = _mapper.Map<List<OrderModel>>(items);
                var totalRecords = orders.Count();

                var pageEntity = new PageEntity<OrderModel>
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
            try
            {

                var Order = await _unitOfWork.OrderRepository.GetByID(id);

                if (Order == null)
                {
                    return new BusinessResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA_MSG);
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, Order);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

      

        public async Task<IBusinessResult> Insert(CreateOrder orderModel)
        {
            try
            {
                if (orderModel.UserId == null)
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, "User ID must be valid.");
                }

                var mapEntity = _mapper.Map<Order>(orderModel);
                mapEntity.OrderCode = Guid.NewGuid().ToString();
                mapEntity.ShippingTrackingCode = Guid.NewGuid().ToString();
                mapEntity.TotalProduct = 1;
                mapEntity.Vat = 0.1;

                double totalProductPrice = 0;
                var auPrice = await _unitOfWork.OrderRepository.GetPriceByBidIdAsync(orderModel.BidId, orderModel.UserId);


                mapEntity.TotalPrice = auPrice * mapEntity.TotalProduct
                                       + (mapEntity.Vat.Value * auPrice)
                                       + (mapEntity.ShippingCost ?? 0)
                                       + (mapEntity.ParticipationFee ?? 0)
                                       - (orderModel.Discount ?? 0);

                mapEntity.OrderDate = DateTime.Now;
                mapEntity.Status = (int)OrderStatus.processing;
                mapEntity.OrderDetails = new List<OrderDetail>()
                {
                    new OrderDetail() 
                    {
                        Price = auPrice.Value,
                        BidId = orderModel.BidId,
                        OrderId = mapEntity.OrderId,
                    }
                 
                };

                if (string.IsNullOrEmpty(orderModel.ShippingAddress))
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, "Shipping address is required.");
                }

                await _unitOfWork.OrderRepository.Insert(mapEntity);
                var result = await _unitOfWork.SaveAsync();


            

                if (result > 0)
                {
                    return new BusinessResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new BusinessResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }



        public async Task<IBusinessResult> Update(int orderId, UpdateOrder orderModel)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderWithDetailsByIdAsync(orderId);

            if (order == null)
            {
                return new BusinessResult(Const.FAIL_UPDATE_CODE, "Order not found.");
            }


            order.Status = orderModel.Status;
            order.TaxCode = orderModel.TaxCode;
            order.ShippingAddress = orderModel.ShippingAddress;
            order.DeliveryDate = orderModel.DeliveryDate;
            order.Note = orderModel.Note;
            order.ShippingCost = orderModel.ShippingCost;
            order.ShippingMethod = orderModel.ShippingMethod;
            order.Discount = orderModel.Discount;
            order.ParticipationFee = orderModel.ParticipationFee;

            double? ProductPrice = order.OrderDetails.FirstOrDefault()?.Price;

            order.TotalPrice = ProductPrice * order.TotalProduct
                                          + (order.Vat * ProductPrice)
                                          + (orderModel.ShippingCost ?? 0)
                                          + (orderModel.ParticipationFee ?? 0)
                                          - (orderModel.Discount ?? 0);
            _unitOfWork.OrderRepository.Update(order);

            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            if (result)
            {
                return new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
            }

            return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
        }


    }
}
