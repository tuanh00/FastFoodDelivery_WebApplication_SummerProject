using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel;
using Data_Layer.ResourceModel.ViewModel.Category;
using Data_Layer.ResourceModel.ViewModel.MenuFoodItemVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public interface ICategoryService
    {
        Task<APIResponseModel> GetCategoryAsync();
        Task<APIResponseModel> GetCategoryByIdsAsync(Guid categoryId);
        Task<APIResponseModel> CreateCategoryAsync(CategoryCreateVM category);
        Task<APIResponseModel> UpdateCategoryAsync(Guid id, CategoryUpdateVM category);
        Task<APIResponseModel> DeleteCategory(Guid id);
    }
}
