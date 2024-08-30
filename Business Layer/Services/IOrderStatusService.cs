using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel;
using Data_Layer.ResourceModel.ViewModel.OrderStatusVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public interface IOrderStatusService
    {
        Task<APIResponseModel> GetOrderStatusAsync();
        Task<APIResponseModel> GetOrderStatusByIdsAsync(Guid orderStatusId);
        Task<APIResponseModel> CreateOrderStatusAsync(OrderStatusVM orderStatusVM);
        Task<APIResponseModel> UpdateOrderStatusAsync(Guid id, OrderStatusUpdateVM  orderStatusUpdateVM);
        Task<APIResponseModel> DeleteOrderStatus(Guid id);
    }
}
