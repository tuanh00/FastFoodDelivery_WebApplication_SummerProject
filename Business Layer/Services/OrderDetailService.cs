using AutoMapper;
using Business_Layer.Repositories;
using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.OrderDetailVMs;

namespace Business_Layer.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;
        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IMapper mapper) 
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        public async Task<APIResponseModel> CreateOrderDetailAsync(OrderDetaiCreateVM orderDetail)
        {
            APIResponseModel reponse = new APIResponseModel();
            try
            {
                var orderEntity = _mapper.Map<OrderDetail>(orderDetail);
                await _orderDetailRepository.AddAsync(orderEntity);
                if (await _orderDetailRepository.SaveAsync() > 0)
                {
                    reponse.Data = _mapper.Map<OrderDetailViewVM>(orderEntity);
                    reponse.IsSuccess = true;
                    reponse.message = "Create new OrderDetail successfully";
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = ex.Message;
            }
            return reponse;
        }

        public async Task<APIResponseModel> DeletedOrderDetailRange(Guid orderid)
        {
            var reponse = new APIResponseModel();
            try
            {
                var orderDetails = await _orderDetailRepository.GetAllAsync();
                var filterOrderDetails = orderDetails.Where(x => x.OrderId == orderid).ToList();

                if (filterOrderDetails == null)
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Not found orderdetail, orderdeil into order is null";
                }
                else
                {
                    _orderDetailRepository.SoftRemoveRange(filterOrderDetails);
                    if (await _orderDetailRepository.SaveAsync() > 0)
                    {
                        var orderDetail = await _orderDetailRepository.GetAllAsync();
                        var filterOrderDetailAfterDeleted = orderDetail.Where(x => x.OrderId == orderid).ToList();
                        var orderDTOAfterUpdate = _mapper.Map<IEnumerable<OrderDetailViewVM>>(filterOrderDetailAfterDeleted);
                        reponse.Data = orderDTOAfterUpdate;
                        reponse.IsSuccess = true;
                        reponse.message = "deleted order detail successfully";
                    }
                    else
                    {
                        reponse.IsSuccess = false;
                        reponse.message = "Update order detail fail!";
                    }

                }
            }
            catch (Exception e)
            {
                reponse.IsSuccess = false;
                reponse.message = $"Update order detail fail!, exception { e.Message }";
            }

            return reponse;
        }

        public async Task<APIResponseModel> GetOrderDetailByIdAsync(Guid orderDetailId)
        {
            var reponse = new APIResponseModel();
            try
            {
                var c = await _orderDetailRepository.GetByIdAsync(orderDetailId);
                if (c == null)
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Don't Have Any Order Detail";
                }
                else
                {
                    reponse.Data = _mapper.Map<OrderDetailViewVM>(c);
                    reponse.IsSuccess = true;
                    reponse.message = "Order Detail Retrieved Successfully";
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = ex.Message;
            }
            return reponse;
        }

        public async Task<APIResponseModel> GetOrderDetailByOrderIdsAsync(Guid orderId)
        {

            APIResponseModel reponse = new APIResponseModel();
            try
            {
                var c = await _orderDetailRepository.GetAllAsync();

                if (c == null)
                {
                    reponse.IsSuccess = false;
                    reponse.message = $"Don't Have Any Order Detail in system.";
                }
                else
                {
                    var filterOrderByOId = c.Where(x => x.OrderId == orderId).ToList();
                    if (filterOrderByOId == null || filterOrderByOId.Count <= 0)
                    {
                        reponse.IsSuccess = false;
                        reponse.message = $"Don't Have Any Order Detail In Order Have Id = {orderId}";
                    }
                    else
                    {
                        reponse.Data = _mapper.Map<IEnumerable<OrderDetailViewVM>>(filterOrderByOId);
                        reponse.IsSuccess = true;
                        reponse.message = "Order Detail Retrieved Successfully";
                    }
                }
            }
            catch (Exception e)
            {
                reponse.IsSuccess = false;
                reponse.message = e.Message;
            }
            return reponse;
        }

        public async Task<APIResponseModel> GetOrderDetailsAsync()
        {
            APIResponseModel reponse = new APIResponseModel();
            try
            {
                var c = await _orderDetailRepository.GetAllAsync();

                if (c == null || c.Count <= 0)
                {
                    reponse.IsSuccess = false;
                    reponse.message = $"Don't Have Any Order Detail";

                }
                else
                {
                    reponse.Data = _mapper.Map<IEnumerable<OrderDetailViewVM>>(c);
                    reponse.IsSuccess = true;
                    reponse.message = "Order Detail Retrieved Successfully";
                }
            }
            catch (Exception e)
            {
                reponse.IsSuccess = false;
                reponse.message = e.Message;
            }
            return reponse;
        }

        public async Task<APIResponseModel> UpdateOrderDetailAsync(Guid id, OrderDetailUpdateVM orderDetail)
        {
            var reponse = new APIResponseModel();
            try
            {
                var orderDetailChecked = await _orderDetailRepository.GetByIdAsync(id);

                if (orderDetailChecked == null)
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Not found order, you are sure input, please checked orderdetailid";
                }
                else
                {

                    var orderDetailFofUpdate = _mapper.Map(orderDetail, orderDetailChecked);
                    var orderDetailDTOAfterUpdate = _mapper.Map<OrderDetailViewVM>(orderDetailFofUpdate);
                    if (await _orderDetailRepository.SaveAsync() > 0)
                    {
                        reponse.Data = orderDetailDTOAfterUpdate;
                        reponse.IsSuccess = true;
                        reponse.message = "Update order detail successfully";
                    }
                    else
                    {
                        reponse.IsSuccess = false;
                        reponse.message = "Update order detail fail!";
                    }
                }
            }
            catch (Exception e)
            {
                reponse.IsSuccess = false;
                reponse.message = $"Update order detail fail!, exception { e.Message }";
            }

            return reponse;
        }
    }
}
