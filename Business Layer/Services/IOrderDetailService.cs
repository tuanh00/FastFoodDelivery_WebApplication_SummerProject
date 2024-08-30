
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.OrderDetailVMs;

namespace Business_Layer.Services
{
    public interface IOrderDetailService
    {
        Task<APIResponseModel> GetOrderDetailsAsync();
        Task<APIResponseModel> GetOrderDetailByOrderIdsAsync(Guid orderId);
        Task<APIResponseModel> GetOrderDetailByIdAsync(Guid orderDetailId);
        Task<APIResponseModel> CreateOrderDetailAsync(OrderDetaiCreateVM orderDetail);
        Task<APIResponseModel> UpdateOrderDetailAsync(Guid id, OrderDetailUpdateVM orderDetail);
        Task<APIResponseModel> DeletedOrderDetailRange(Guid orderid);
    }
}
