using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.CartVMs;
namespace Business_Layer.Services
{
    public interface ICartService
    {
        Task<APIResponseModel> GetCartsByUserIdAsync(Guid userID);
        Task<APIResponseModel> CreateCartAsync(CartCreateVM createdto);
        Task<APIResponseModel> UpdateQuanityCartAsync(Guid id, CartUpdateVM updatedto);
        Task<APIResponseModel> DeleteCartAsync(Guid id);
    }
}
