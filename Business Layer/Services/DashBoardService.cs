using Business_Layer.Repositories;
using Data_Layer.Models;
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
    public class DashBoardService : IDashboardService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IUserRepository _userRepository;
        //private readonly IShipperRepository _shipperRepository;
        private readonly IMenuFoodItem1Repository _menuFoodItem1Repository;
        private readonly ICategoryRepository _categoryRepository;

        public DashBoardService(IOrderRepository order,
            IOrderDetailRepository orderDetail,
            IUserRepository userRepository,
            //IShipperRepository shipperRepository,
            IMenuFoodItem1Repository menuFoodItem1Repository,
            ICategoryRepository categoryRepository)
        {
            _orderRepository = order;
            _orderDetailRepository = orderDetail;
            _userRepository = userRepository;
            //_shipperRepository = shipperRepository;
            _menuFoodItem1Repository = menuFoodItem1Repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<decimal> GetTotalSalesByMonth(int month, int year)
        {
            var orders = await _orderRepository.GetConfirmedOrders();
            var totalSalesByMonth = orders.Where(order => order.OrderDate.Month == month
                                            && order.OrderDate.Year == year)
                                    .Sum(order => order.TotalPrice);
            return totalSalesByMonth.GetValueOrDefault();
        }
        public async Task<decimal> GetTotalSalesByYear(int year)
        {
            var orders = await _orderRepository.GetConfirmedOrders();
            var totalSalesByYear = orders.Where(order => order.OrderDate.Year == year)
                                          .Sum(order => order.TotalPrice);
            return totalSalesByYear.GetValueOrDefault();
        }
        public async Task<decimal> GetTotalRevenue()
        {
            var orders = await _orderRepository.GetConfirmedOrders();
            decimal? totalRevenue = orders.Sum(order => order.TotalPrice);
            return totalRevenue.GetValueOrDefault();
        }
        public async Task<decimal> GetTotalSalesByWeek(int year, int weekNumber)
        {
            var orders = await _orderRepository.GetConfirmedOrders();
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
            DateTime firstMonday = jan1.AddDays(daysOffset);
            var cal = System.Globalization.CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(jan1, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            var weekNum = weekNumber;

            // If the first week of the year starts before the first Monday, adjust week number
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }

            DateTime weekStart = firstMonday.AddDays(weekNum * 7);
            DateTime weekEnd = weekStart.AddDays(7).AddTicks(-1); // End of the week

            var weeklyTotalSales = orders.Where(o => o.OrderDate >= weekStart
            &&  o.OrderDate <= weekEnd).Sum(o => o.TotalPrice);
            return weeklyTotalSales.GetValueOrDefault();
        }
        public async Task<List<LoyalCustomer>> GetTopLoyalCustomer()
        {
            var loyalCustomeList = await _userRepository.GetTopFiveCustomerAsync();
            return loyalCustomeList;
        }
        public async Task<APIGenericReposneModel<int>> GetTotalActiveUser()
        {
            var response = new APIGenericReposneModel<int>();
            var users = await _userRepository.GetAllAsync();
            var activeUser = users.Where(u => u.Status == "Active").Count();
            if (activeUser > 0)
            {
                response.code = 200;
                response.IsSuccess = true;
                response.message = "Total Active User !";
                response.Data = activeUser;
            }
            else
            {
                response.code = 500;
                response.IsSuccess = false;
                response.message = "Can not find active user !";
            }
            return response;
        }

        public async Task<APIGenericReposneModel<int>> CountTotalOrder()
        {
            var response = new APIGenericReposneModel<int>();
            var orders = await _orderRepository.GetConfirmedOrders();
            var total = orders.Count();
            if(total > 0)
            {
                response.code = 200;
                response.message = "Total Orders !";
                response.IsSuccess = true;
                response.Data = total;
            }
            else
            {
                response.code = 500;
                response.message = "There is no any order !";
                response.IsSuccess= false;
            }
            return response;
        }
      /*  public async Task<APIGenericReposneModel<List<ShipperReport>>>? GetTopFiveShippersAsync()
        {
            var response = new APIGenericReposneModel<List<ShipperReport>>();
            response.Data = await _shipperRepository.GetTopFiveShippersAsync();
            if (response.Data is null)
            {
                response.code = 500;
                response.IsSuccess = false;
                response.message = "There is not any shipped order !";                
            }
            else
            {
                response.code = 200;
                response.IsSuccess = true;
                response.message = "Top 5 shippers !";
            }
            return response;
        } */ 

        public async Task<APIGenericReposneModel<List<MostSalesFood>>> GetTopSalesFood()
        {
            var response = new APIGenericReposneModel<List<MostSalesFood>>();
            response.Data = await _menuFoodItem1Repository.GetTopSalesFood();
            if (response.Data is null)
            {
                response.code = 500;
                response.message = "List of sale food is empty !";
                response.IsSuccess = false;
            }
            else
            {
                response.code = 200;
                response.message = "Top Sales Food !";
                response.IsSuccess = true;
            }
            return response;
        }

        public async Task<APIGenericReposneModel<int>> GetTotalCategories()
        {
            var response = new APIGenericReposneModel<int>
            {
                message = "Total Categories !",
                IsSuccess = true,
                code = 200,
                Data = await _categoryRepository.GetTotalCategories()
            };
            return response;
        }

        public async Task<APIGenericReposneModel<int>> GetTotalFood()
        {
            var response = new APIGenericReposneModel<int>
            {
                message = "Total Categories !",
                IsSuccess = true,
                code = 200,
                Data = await _menuFoodItem1Repository.GetTotalFood()
            };
            return response;
        }

        public Task<APIGenericReposneModel<List<ShipperReport>>>? GetTopFiveShippersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
