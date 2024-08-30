using AutoMapper;
using Business_Layer.Repositories;
using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.Enum;
using Data_Layer.ResourceModel.ViewModel.OrderStatusVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly IMapper _mapper;

        public OrderStatusService(IOrderStatusRepository orderStatusRepository, IMapper mapper)
        {
            _orderStatusRepository = orderStatusRepository;
            _mapper = mapper;
        }

        public async Task<APIResponseModel> CreateOrderStatusAsync(OrderStatusVM orderStatusVM)
        {
            APIResponseModel reponse = new APIResponseModel();
            try
            {
                var Entity =  _mapper.Map<OrderStatus>(orderStatusVM);
                Entity.OrderStatusName =  OrderStatusEnum.Processing.ToString();
                await _orderStatusRepository.AddAsync(Entity);
                if(await _orderStatusRepository.SaveAsync() > 0)
                {
                    reponse.Data = _mapper.Map<OrderStatusVM>(Entity);
                    reponse.IsSuccess = true;
                    reponse.message = "Create OrderStatus Successfully";
                }
                else
                {
                    reponse.code = 400;
                    reponse.IsSuccess = false;
                    reponse.message = "Create OrderStatus Fail";
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess= false;
                reponse.message = ex.Message;
            }
            return reponse;
        }

        public Task<APIResponseModel> CreateOrderStatusAsync(OrderStatusUpdateVM orderStatusUpdateVM)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponseModel> DeleteOrderStatus(Guid orderStatusid)
        {
            var reponse = new APIResponseModel();
            
            try
            {
                var orderStatusChecked = await _orderStatusRepository.GetByIdAsync(orderStatusid);
                if(orderStatusChecked == null)
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Not Found Order Status, you are sure input";
                }else if(orderStatusChecked.OrderStatusName == "IsDeleted"){
                    reponse.IsSuccess = false;
                    reponse.message = "OrderStatus is Deleted, can not delete orderStatus again.";
                }
                else
                {
                    orderStatusChecked.OrderStatusName = "IsDeleted";
                    var orderStatusUpdate = _mapper.Map<OrderStatusViewVM>(orderStatusChecked);
                    var orderDTOAfterUpdate = _mapper.Map<OrderStatusViewVM>(orderStatusUpdate);
                    if(await _orderStatusRepository.SaveAsync() > 0)
                    {
                        reponse.Data = orderDTOAfterUpdate;
                        reponse.IsSuccess = true;
                        reponse.message = "Delete order Status Successfull";
                    }
                    else
                    {
                        reponse.Data = orderDTOAfterUpdate;
                        reponse.IsSuccess = false;
                        reponse.message = "Delete Order Status Fail";
                    }
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = $"Delete OrderStatus fail!, exception {ex.Message}";
            }
            return reponse;
        }

        public async Task<APIResponseModel> GetOrderStatusAsync()
        {
           var reponse = new APIResponseModel();
            List<OrderStatusViewVM> OrderStatusDTOs = new List<OrderStatusViewVM>();
            try
            {
                var orderStatusChecked = await _orderStatusRepository.GetAllAsync(x => x.Order);
                foreach(var orderStatus in orderStatusChecked)
                {
                    var EntityDTO = _mapper.Map<OrderStatusViewVM>(orderStatus);
                    EntityDTO.TotalPrice = orderStatus.Order.TotalPrice;
                    EntityDTO.RequiredDate = orderStatus.Order.RequiredDate;
                    EntityDTO.OrderDate = orderStatus.Order.OrderDate;
                    EntityDTO.Address = orderStatus.Order.Address;
                    EntityDTO.ShippedDate = orderStatus.Order.ShippedDate;
                    OrderStatusDTOs.Add(EntityDTO);
                }
                if(OrderStatusDTOs.Count > 0)
                {
                    reponse.Data = OrderStatusDTOs;
                    reponse.IsSuccess = true;
                    reponse.message = $"Have {OrderStatusDTOs.Count} OrderStatus.";
                    return reponse;
                }
                else
                {
                    reponse.Data = OrderStatusDTOs;
                    reponse.IsSuccess = false;
                    reponse.message = $"Have {OrderStatusDTOs.Count} OrderStatus";
                    return reponse;
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess =false;
                reponse.message = ex.Message;
                return reponse;
            }
        }

        public async Task<APIResponseModel> GetOrderStatusByIdsAsync(Guid orderStatusId)
        {
            var reponse = new APIResponseModel();
            try
            {
                var c = await _orderStatusRepository.GetByIdAsync(orderStatusId, x => x.Order);
                if(c == null)
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Don't Have Any OrderStatus";
                }
                else
                {
                    var EntityDTO = _mapper.Map<OrderStatusViewVM>(c);
                    EntityDTO.TotalPrice = c.Order.TotalPrice;
                    EntityDTO.Address = c.Order.Address;
                    EntityDTO.OrderDate = c.Order.OrderDate;
                    EntityDTO.ShippedDate = c.Order.ShippedDate;
                    EntityDTO.RequiredDate = c.Order.RequiredDate;
                    reponse.Data = EntityDTO;
                    reponse.IsSuccess = true;
                    reponse.message = "Order Status Retrieved Successfully";
                }
            }
            catch(Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = ex.Message;
            }
            return reponse;
        }

        public async Task<APIResponseModel> UpdateOrderStatusAsync(Guid id, OrderStatusUpdateVM orderStatusUpdateVM)
        {
            var reponse = new APIResponseModel();
            try
            {
                var orderStatusChecked = await _orderStatusRepository.GetByIdAsync(id);
                if(orderStatusChecked == null) 
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Not Found OrderStatus, you are sure input";
                }
                else
                {
                    var orderStatusUpdate = _mapper.Map(orderStatusUpdateVM, orderStatusChecked);
                    var orderDTOAfterUpdate = _mapper.Map<OrderStatusViewVM>(orderStatusUpdate);
                    if(await _orderStatusRepository.SaveAsync() > 0)
                    {
                        reponse.Data = orderDTOAfterUpdate;
                        reponse.IsSuccess = true;
                        reponse.message = "Update OrderStatus successfully";
                    }
                    else
                    {
                        reponse.Data = orderDTOAfterUpdate;
                        reponse.IsSuccess = false;
                        reponse.message = "Update Order Status fail!";
                    }
                }
            }catch(Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = $"Update food fail!, exception {ex.Message}";
            }
            return reponse;
        }
    }
}
