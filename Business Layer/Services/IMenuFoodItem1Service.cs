using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.MenuFoodItemVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public interface IMenuFoodItem1Service
    {
        Task<APIResponseModel> GetFoodsAsync();
        Task<APIResponseModel> GetFoodByIdsAsync(Guid foodId);
        Task<APIResponseModel> GetFoodsByCategoryIdAsync(Guid categoryId);
        Task<APIResponseModel> SearchFoodsAsync(string searchTerm);
        Task<APIResponseModel> CreateFoodAsync(MenuFoodItemCreateVM createdto);
        Task<APIResponseModel> UpdateFoodAsync(Guid id, MenuFoodItemUpdateVM updatedto);
        Task<APIResponseModel> DeleteFood(Guid id);
        
    }
}
