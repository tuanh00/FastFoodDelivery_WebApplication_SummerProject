using Business_Layer.DataAccess;
using Data_Layer.Models;
using Data_Layer.ResourceModel.ViewModel;
using Data_Layer.ResourceModel.ViewModel.Enum;
using Data_Layer.ResourceModel.ViewModel.MenuFoodItemVMs;
using Microsoft.EntityFrameworkCore;

namespace Business_Layer.Repositories
{
    public class MenuFoodItem1Repository : GenericRepository<MenuFoodItem>, IMenuFoodItem1Repository
    {
        private readonly FastFoodDeliveryDBContext _dbContext;
        public MenuFoodItem1Repository(FastFoodDeliveryDBContext context, FastFoodDeliveryDBContext dbContext) : base(context)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MenuFoodItem>> GetMenuFoodItemAll()
        {
            var menuFoodItemlists = await _dbContext.MenuFoodItems.Where(x => x.FoodStatus == MenuFoodItemStatusEnum.Active.ToString()).ToListAsync();
            return menuFoodItemlists;
        }

        public async Task<IEnumerable<MenuFoodItem>> SearchMenuFoodItems(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _dbContext.MenuFoodItems
                    .Where(x => x.FoodStatus == MenuFoodItemStatusEnum.Active.ToString())
                    .ToListAsync();
            }

            searchTerm = searchTerm.ToLower();

            var menuFoodItemLists = await _dbContext.MenuFoodItems
                .Where(x => x.FoodStatus == MenuFoodItemStatusEnum.Active.ToString() &&
                            (x.FoodName.ToLower().Contains(searchTerm) ||
                             x.FoodDescription.ToLower().Contains(searchTerm))) // Assuming a navigation property to Category
                .ToListAsync();

            return menuFoodItemLists;
        }

        public async Task<List<MostSalesFood>> GetTopSalesFood()
        {
            var menuFoodItems = await _dbContext.MenuFoodItems.Include(m => m.Category)
                .Include(m => m.OrderDetails).ToListAsync();
            var topSalesFood = menuFoodItems.Select(food => new MostSalesFood
            {
                FoodName = food.FoodName,
                Category = food.Category.CategoriesName,
                Quantity = food.OrderDetails.Count(order => order.FoodId == food.FoodId)
            })
                .OrderByDescending(x => x.Quantity)
                .Take(5)
                .ToList();
            return topSalesFood;
        }

        public async Task<int> GetTotalFood()
        {
            var totalFood = await _dbContext.MenuFoodItems.Where(m => m.FoodStatus == "Active").CountAsync();
            return totalFood;
        }
    }
}
