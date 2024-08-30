using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.DashboardViewModel;
using Data_Layer.ResourceModel.ViewModel.MenuFoodItemVMs;
using Data_Layer.ResourceModel.ViewModel.ShipperViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public interface IDashboardService
    {
        public Task<decimal> GetTotalSalesByMonth(int month, int year);
        public Task<decimal> GetTotalSalesByYear(int year);
        public Task<decimal> GetTotalRevenue();
        public Task<decimal> GetTotalSalesByWeek(int year, int weekNumber);
        public Task<List<LoyalCustomer>> GetTopLoyalCustomer();
        public Task<APIGenericReposneModel<int>> GetTotalActiveUser();
        public Task<APIGenericReposneModel<int>> CountTotalOrder();

        /*  public Task<APIGenericReposneModel<List<ShipperReport>>>? GetTopFiveShippersAsync(); */
        public Task<APIGenericReposneModel<List<MostSalesFood>>> GetTopSalesFood();
        public Task<APIGenericReposneModel<int>> GetTotalFood();
        public Task<APIGenericReposneModel<int>> GetTotalCategories();
    }
}
