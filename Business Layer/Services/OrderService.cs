using AutoMapper;
using Business_Layer.Repositories;
using Business_Layer.Services.VNPay;
using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.Enum;
using Data_Layer.ResourceModel.ViewModel.OrderVMs;
using Stripe.Climate;

namespace Business_Layer.Services
{
    //Pending = 0,
    //Confirmed = 1,
    //Processing = 2,
    //Shipped = 3,
    //Delivered = 4,
    //Cancelled = 5,
    //Returned = 6,
    //Failed = 7
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMenuFoodItem1Repository _menuFoodItem1Repository;
        private readonly IUserSerivce _userSerivce;
        private readonly IVNPayService _vNPayService;
        private readonly IUserRepository _userRepository;

        public OrderService(IMapper mapper, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IMenuFoodItem1Repository menuFoodItem1Repository, IVNPayService vNPayService, IUserSerivce userSerivce)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _menuFoodItem1Repository = menuFoodItem1Repository;
            _vNPayService = vNPayService;
            _userSerivce = userSerivce;
        }

        public async Task<APIResponseModel> CancelOrderAsync(Guid id)
        {
            var reponse = new APIResponseModel();
            try
            {
                var orderChecked = await _orderRepository.GetByIdAsync(id);

                if (orderChecked == null)
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Not found order, you are sure input";
                }
                else if (orderChecked.StatusOrder == "Cancelled")
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Order are cancel, can not cancel order again.";
                }
                else if (orderChecked.StatusOrder == "Confirmed")
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Order is confirm, can not cancel order.";
                }
                else
                {
                    
                        orderChecked.StatusOrder = "Cancelled";
                        var orderFofUpdate = _mapper.Map<OrderViewVM>(orderChecked);
                        var orderDTOAfterUpdate = _mapper.Map<OrderViewVM>(orderFofUpdate);
                        if (await _orderRepository.SaveAsync() > 0)
                        {
                            reponse.Data = orderDTOAfterUpdate;
                            reponse.IsSuccess = true;
                            reponse.message = "Update order successfully";
                        }
                        else
                        {
                            reponse.Data = orderDTOAfterUpdate;
                            reponse.IsSuccess = false;
                            reponse.message = "Update order fail!";
                        }
                }
            }
            catch (Exception e)
            {
                reponse.IsSuccess = false;
                reponse.message = $"Update order fail!, exception {e.Message}";
            }

            return reponse;
        }

        public async Task<APIResponseModel> CancelOrderForShipperAsync(Guid id, OrderUpdateForShipperVM updatedto)
        {
            var reponse = new APIResponseModel();
            try
            {
                var orderChecked = await _orderRepository.GetByIdAsync(id);

                if (orderChecked == null && orderChecked.StatusOrder == "Cancelled")
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Not found order, you are sure input";
                }
                else
                {
                    if (orderChecked.DeliveryStatus == DeliveryStatusEnum.InTransit.ToString() || orderChecked.DeliveryStatus == DeliveryStatusEnum.Received.ToString())
                    {
                        var orderFofUpdate = _mapper.Map(updatedto, orderChecked);
                        orderChecked.DeliveryStatus = DeliveryStatusEnum.Cancelled.ToString();
                        var orderDTOAfterUpdate = _mapper.Map<OrderViewVM>(orderFofUpdate);
                        if (await _orderRepository.SaveAsync() > 0)
                        {
                            reponse.Data = orderDTOAfterUpdate;
                            reponse.IsSuccess = true;
                            reponse.message = "Update status delivery of order successfully";
                        }
                        else
                        {
                            reponse.Data = orderDTOAfterUpdate;
                            reponse.IsSuccess = false;
                            reponse.message = "Update status delivery of order fail!";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                reponse.IsSuccess = false;
                reponse.message = $"Update order fail!, exception {e.Message}";
            }

            return reponse;
        }

        public async Task<APIResponseModel> CancelOrderForShipperAsync(Guid Id)
        {
            var response = new APIResponseModel();
            try
            {
                var orderChecked = await _orderRepository.GetByIdAsync(Id);
                if (orderChecked == null)
                {

                    response.IsSuccess = false;
                    response.message = "Not found order, you are sure input";
                }
                else if (orderChecked.DeliveryStatus == "Received" || orderChecked.DeliveryStatus == "Delivered")
                {
                    response.IsSuccess = false;
                    response.message = "Bill is delivered";
                }
                else if (orderChecked.DeliveryStatus == "Cancelled")
                {
                    response.IsSuccess = false;
                    response.message = "Bill is Cancelled";
                }
                else
                {
                    orderChecked.DeliveryStatus = "Cancelled";
                    var orderofUpdate = _mapper.Map<OrderViewVM>(orderChecked);
                    var orderDTOAfterUpdate = _mapper.Map<OrderViewVM>(orderofUpdate);
                    if (await _orderRepository.SaveAsync() > 0)
                    {
                        response.Data = orderDTOAfterUpdate;
                        response.IsSuccess = true;
                        response.message = "Update Order successfully";
                    }
                    else
                    {
                        response.Data = orderDTOAfterUpdate;
                        response.IsSuccess = false;
                        response.message = "Update Order fail!";
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.message = $"Confirm food Fail!, exception {ex.Message}";
            }
            return response;
        }

        //public async Task<APIResponseModel> CheckoutAsync(OrderCreateVM orderdto, List<OrderDetaiCreateVM> orderDetaildto)
        //{
        //    var response = new APIResponseModel();
        //    try
        //    {
        //        var orderDetailEntity = _mapper.Map<List<OrderDetail>>(orderDetaildto);
        //        decimal totalPrice = 0;
        //        if (orderDetailEntity.Count > 0)
        //        {
        //            foreach (var orderDetail in orderDetailEntity)
        //            {
        //                totalPrice += (decimal)(orderDetail.UnitPrice * orderDetail.Quantity);
        //            }
        //            var orderEntity = _mapper.Map<Order>(orderdto);
        //            orderEntity.StatusOrder = "Pending";
        //            orderEntity.TotalPrice = totalPrice;
        //            await _orderRepository.AddAsync(orderEntity);

        //            if (await _orderRepository.SaveAsync() > 0)
        //            {
        //                foreach (OrderDetail od in orderDetailEntity)
        //                {
        //                    od.OrderId = orderEntity.OrderId;
        //                    od.UnitPrice = _menuFoodItem1Repository.GetByIdAsync(od.FoodId.GetValueOrDefault()).Result.UnitPrice;
        //                    await _orderDetailRepository.AddAsync(od);
        //                }

        //                if (await _orderDetailRepository.SaveAsync() > 0)
        //                {
        //                    var payment = _vNPayService.CreatePaymentRequestAsync(orderEntity.OrderId).ToString();
        //                    if (payment != null)
        //                    {
        //                        response.IsSuccess = true;
        //                        response.message = payment.ToString();
        //                    }
        //                    else
        //                    {
        //                        response.IsSuccess = false;
        //                        response.message = "payment fail"; 
        //                    }
                            
        //                }
        //                else
        //                {
        //                    response.IsSuccess = false;
        //                    response.message = "Failed to add Order Details.";
        //                }
        //            }
        //            else
        //            {
        //                response.IsSuccess = false;
        //                response.message = "Failed to add Order.";
        //            }
        //        }
        //        else
        //        {
        //            response.IsSuccess = false;
        //            response.message = "No products found for checkout. Please add one or more products.";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        response.IsSuccess = false;
        //        response.message = $"Order creation failed! Exception: {e.Message}";
        //    }

        //    return response;
        //}

        public async Task<APIResponseModel> ConfirmOrderForShipperAsync(Guid Id)
        {
            var response = new APIResponseModel();
            try
            {
                var orderChecked = await _orderRepository.GetByIdAsync(Id);
                if (orderChecked == null)
                {

                    response.IsSuccess = false;
                    response.message = "Not found order, you are sure input";
                }
                else if (orderChecked.DeliveryStatus == "Received" || orderChecked.DeliveryStatus == "Delivered")
                {
                    response.IsSuccess = false;
                    response.message = "Bill is delivered";
                }
                else
                {
                    orderChecked.DeliveryStatus = "Delivered";
                    var orderofUpdate = _mapper.Map<OrderViewVM>(orderChecked);
                    var orderDTOAfterUpdate = _mapper.Map<OrderViewVM>(orderofUpdate);
                    if (await _orderRepository.SaveAsync() > 0)
                    {
                        response.Data = orderDTOAfterUpdate;
                        response.IsSuccess = true;
                        response.message = "Update Order successfully";
                    }
                    else
                    {
                        response.Data = orderDTOAfterUpdate;
                        response.IsSuccess = false;
                        response.message = "Update Order fail!";
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.message = $"Confirm food Fail!, exception {ex.Message}";
            }
            return response;
        }

        public async Task<APIResponseModel> ConfirmOrderForUserAsync(Guid Id)
        {
           var response = new APIResponseModel();
            try
            {
                var orderChecked = await _orderRepository.GetByIdAsync(Id);
                if (orderChecked == null) {
                
                      response.IsSuccess = false;
                    response.message = "Not found order, you are sure input";
                }else if(orderChecked.DeliveryStatus == "Received")
                {
                    response.IsSuccess = false;
                    response.message = "Bill is recieved";
                }
                else
                {
                    orderChecked.DeliveryStatus = "Received";
                    var orderofUpdate = _mapper.Map<OrderViewVM>(orderChecked);
                    var orderDTOAfterUpdate = _mapper.Map<OrderViewVM>(orderofUpdate);
                    if(await _orderRepository.SaveAsync() > 0)
                    {
                        response.Data = orderDTOAfterUpdate;
                        response.IsSuccess = true;
                        response.message = "Update Order successfully";
                    }
                    else
                    {
                        response.Data = orderDTOAfterUpdate;
                        response.IsSuccess = false;
                        response.message = "Update Order fail!";
                    }
                }
            }catch (Exception ex)
            {
                response.IsSuccess = false;
                response.message = $"Confirm food Fail!, exception {ex.Message}";
            }
            return response;
        }

        public async Task<APIResponseModel> CreateOrderAsync(OrderCreateVM createdto)
        {
            var reponse = new APIResponseModel();

            try
            {
                var customer = _userSerivce.GetUserById(createdto.MemberId);
                var orderentity = _mapper.Map<Data_Layer.Models.Order>(createdto);
                orderentity.StatusOrder = "Pending";
                orderentity.Address = customer.Result.Address;
                orderentity.DeliveryStatus= DeliveryStatusEnum.Processing.ToString();
                await _orderRepository.AddAsync(orderentity);

                if (await _orderRepository.SaveAsync() > 0)
                {
                    var paymentUrl = await _vNPayService.CreatePaymentRequestAsync(orderentity.OrderId);
                    if (!string.IsNullOrEmpty(paymentUrl))
                    {
                        reponse.Data = _mapper.Map<OrderViewVM>(orderentity);
                        reponse.IsSuccess = true;
                        reponse.message = paymentUrl;
                    }
                    else
                    {
                        reponse.IsSuccess = false;
                        reponse.message = "Payment failed.";
                    }
                    return reponse;
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = ex.Message;
                return reponse;
            }

            return reponse;
        }
        public async Task<APIResponseModel> GetOrderByIdAsync(Guid orderId)
        {
            var _response = new APIResponseModel();
            try
            {
                var c = await _orderRepository.GetByIdAsync(orderId, x => x.User);
                if (c == null)
                {
                    _response.IsSuccess = false;
                    _response.message = "Don't Have Any Order ";
                }
                else
                {
                    var mapper = _mapper.Map<OrderViewVM>(c);
                    mapper.MemberName = c.User.UserName;
                    mapper.PhoneNumber = c.User.PhoneNumber;
                    _response.Data = _mapper.Map<OrderViewVM>(c);
                    _response.IsSuccess = true;
                    _response.message = "Order Retrieved Successfully";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.message = ex.Message;
            }

            return _response;
        }

        public async Task<APIResponseModel> GetOrderByUserIDAsync(Guid userId)
        {
            var reponse = new APIResponseModel();
            List<OrderViewVM> OrderDTOs = new List<OrderViewVM>();
            try
            {
                List<Data_Layer.Models.Order> orders = (await _orderRepository.GetAllOrderByUserIdAsync(userId.ToString())).ToList();
                var user = _userSerivce.GetUserById(userId.ToString());
                foreach (var order in orders)
                {
                    var mapper = _mapper.Map<OrderViewVM>(order);
                    mapper.MemberName = user.Result.FullName;
                    mapper.PhoneNumber = user.Result.PhoneNumber;
                    OrderDTOs.Add(mapper);
                }
                if (OrderDTOs.Count > 0)
                {
                    reponse.Data = OrderDTOs;
                    reponse.IsSuccess = true;
                    reponse.message = $"Have {OrderDTOs.Count} order.";
                    return reponse;
                }
                else
                {
                    reponse.IsSuccess = false;
                    reponse.message = $"Have {OrderDTOs.Count} order. Order is null, not found";
                    return reponse;
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = "Exception";
                return reponse;
            }
        }

        public async Task<APIResponseModel> GetOrdersAsync()
        {
            var reponse = new APIResponseModel();
            List<OrderViewVM> OrderDTOs = new List<OrderViewVM>();
            try
            {
                var orders = await _orderRepository.GetAllAsync(x => x.User);
                foreach (var order in orders)
                {
                    var mapper = _mapper.Map<OrderViewVM>(order);
                    mapper.MemberName = order.User.UserName;
                    mapper.PhoneNumber = order.User.PhoneNumber;
                    OrderDTOs.Add(mapper);
                }
                if (OrderDTOs.Count > 0)
                {
                    reponse.Data = OrderDTOs;
                    reponse.IsSuccess = true;
                    reponse.message = $"Have {OrderDTOs.Count} order.";
                    return reponse;
                }
                else
                {
                    reponse.IsSuccess = false;
                    reponse.message = $"Have {OrderDTOs.Count} order.";
                    return reponse;
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = ex.Message;
                return reponse;
            }
        }

        public async Task<APIResponseModel> GetOrdersAsyncForShipper()
        {
            var reponse = new APIResponseModel();
            List<OrderViewVM> OrderDTOs = new List<OrderViewVM>();
            try
            {
                var orders = await _orderRepository.GetAllAsync();
                var orderFilter = orders.Where(x => x.ShipperId == null).ToList();
                foreach (var order in orderFilter)
                {
                    if (order.StatusOrder.Equals("Paid"))
                    {
                        OrderDTOs.Add(_mapper.Map<OrderViewVM>(order));
                    }
                }
                if (OrderDTOs.Count > 0)
                {
                    reponse.Data = OrderDTOs;
                    reponse.IsSuccess = true;
                    reponse.message = $"Have {OrderDTOs.Count} order for shipper.";
                    return reponse;
                }
                else
                {
                    reponse.IsSuccess = false;
                    reponse.message = $"Have {OrderDTOs.Count} order for shipper.";
                    return reponse;
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = ex.Message;
                return reponse;
            }
        }

        public async Task<APIResponseModel> GetOrdersAsyncOfShipper(Guid shipperId)
        {
            var reponse = new APIResponseModel();
            List<OrderViewVM> OrderDTOs = new List<OrderViewVM>();
            try
            {
                var orders = await _orderRepository.GetAllAsync(x => x.User);
                var orderFilter = orders.Where(x => x.ShipperId == shipperId).ToList();
                foreach (var order in orderFilter)
                {
                    var mapper = _mapper.Map<OrderViewVM>(order);
                    mapper.MemberName = order.User.UserName;
                    mapper.PhoneNumber = order.User.PhoneNumber;
                    mapper.Address = order.User.Address;
                    OrderDTOs.Add(_mapper.Map<OrderViewVM>(mapper));
                }
                if (OrderDTOs.Count > 0)
                {
                    reponse.Data = OrderDTOs;
                    reponse.IsSuccess = true;
                    reponse.message = $"Have {OrderDTOs.Count} order for shipper.";
                    return reponse;
                }
                else
                {
                    reponse.IsSuccess = false;
                    reponse.message = $"Have {OrderDTOs.Count} order for shipper.";
                    return reponse;
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = ex.Message;
                return reponse;
            }
        }

        public Task<APIResponseModel> GetSortedOrdersAsync(string sortName)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponseModel> UpdateOrderAsync(Guid id, OrderUpdateVM updatedto)
        {
            var reponse = new APIResponseModel();
            try
            {
                var orderChecked = await _orderRepository.GetByIdAsync(id);

                if (orderChecked == null && orderChecked.StatusOrder == "Pending")
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Order is Canceled Payment";
                }
                else
                {
                    
                    var orderFofUpdate = _mapper.Map(updatedto, orderChecked);
                    var orderDTOAfterUpdate = _mapper.Map<OrderViewVM>(orderFofUpdate);
                    if (await _orderRepository.SaveAsync() > 0)
                    {
                        reponse.Data = orderDTOAfterUpdate;
                        reponse.IsSuccess = true;
                        reponse.message = "Update order successfully";
                    }
                    else
                    {
                        reponse.Data = orderDTOAfterUpdate;
                        reponse.IsSuccess = false;
                        reponse.message = "Update order fail!";
                    }
                }
            }
            catch (Exception e)
            {
                reponse.IsSuccess = false;
                reponse.message = $"Update order fail!, exception {e.Message}";
            }

            return reponse;
        }

        public async Task<APIResponseModel> UpdateOrderForShipperAsync(Guid id, OrderUpdateForShipperVM updatedto)
        {
            var reponse = new APIResponseModel();
            try
            {
                var orderChecked = await _orderRepository.GetByIdAsync(id);

                if (orderChecked == null && orderChecked.StatusOrder == "Pending")
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Order is Canceled Payment";
                }
                else
                {
                    Data_Layer.Models.Order orderFofUpdate;
                    OrderViewVM orderDTOAfterUpdate;
                    if (orderChecked.ShipperId == null)
                    {
                        orderFofUpdate = _mapper.Map(updatedto, orderChecked);
                        orderChecked.ShipperId = updatedto.ShipperId;
                        orderChecked.DeliveryStatus = DeliveryStatusEnum.InTransit.ToString();
                        orderDTOAfterUpdate = _mapper.Map<OrderViewVM>(orderFofUpdate);
                        if (await _orderRepository.SaveAsync() > 0)
                        {
                            reponse.Data = orderDTOAfterUpdate;
                            reponse.IsSuccess = true;
                            reponse.message = "Update status delivery of order successfully";
                        }
                        else
                        {
                            reponse.Data = orderDTOAfterUpdate;
                            reponse.IsSuccess = false;
                            reponse.message = "Update status delivery of order fail!";
                        }
                    }
                    else
                    {
                        reponse.IsSuccess = false;
                        reponse.message = "Update status delivery of order fail!";
                    }
                   /* else if (orderChecked.DeliveryStatus == DeliveryStatusEnum.InTransit.ToString())
                    {
                        orderFofUpdate = _mapper.Map(updatedto, orderChecked);
                        orderChecked.DeliveryStatus = DeliveryStatusEnum.Delivered.ToString();
                        orderDTOAfterUpdate = _mapper.Map<OrderViewVM>(orderFofUpdate);
                        if (await _orderRepository.SaveAsync() > 0)
                        {
                            reponse.Data = orderDTOAfterUpdate;
                            reponse.IsSuccess = true;
                            reponse.message = "Update status delivery of order successfully";
                        }
                        else
                        {
                            reponse.Data = orderDTOAfterUpdate;
                            reponse.IsSuccess = false;
                            reponse.message = "Update status delivery of order fail!";
                        }
                    } */
                }
            }
            catch (Exception e)
            {
                reponse.IsSuccess = false;
                reponse.message = $"Update order fail!, exception {e.Message}";
            }
            return reponse;
        }

        
    }
}
