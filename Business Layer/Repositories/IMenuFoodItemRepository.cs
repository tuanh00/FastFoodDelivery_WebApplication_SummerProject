using Data_Layer.ResourceModel.ViewModel;
using Data_Layer.ResourceModel.ViewModel.MenuFoodItemVMs;

namespace Business_Layer.Repositories
{
    public interface IMenuFoodItemRepository
    {
        Task<List<MenuFoodItemVM>> GetMenuFoodItem();
        Task<MenuFoodItemVM> GetMenuFoodItemById(Guid Id);
        Task<bool> AddProduct(MenuFoodItemVM menuFoodItemVM);
    }
}
