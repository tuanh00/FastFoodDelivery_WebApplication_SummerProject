using Business_Layer.DataAccess;
using Data_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly FastFoodDeliveryDBContext _dbContext;
        public OrderRepository(FastFoodDeliveryDBContext context, FastFoodDeliveryDBContext dbContext) : base(context)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Order>> GetAllByStatusAsync(string status)
        {
            var Orders = await _dbContext.Orders.Where(o => o.StatusOrder.ToLower() == status.ToLower()).ToListAsync();
            if (Orders.Any() == false)
            {
                throw new Exception("User haven't Order");
            }
            return Orders;
        }

        public async Task<IEnumerable<Order>> GetAllOrderByUserIdAsync(string memberID)
        {
            var Orders = await _dbContext.Orders.Where(o => o.MemberId == memberID).ToListAsync();
            if (Orders.Any() == false)
            {
                throw new Exception("User haven't Order");
            }
            return Orders;

        }

        public async Task<IEnumerable<Order>> GetConfirmedOrders()
        {
            var orders = await _dbContext.Orders.ToListAsync();
            var confirmedOrders = orders.Where(o => o.StatusOrder == "Paid");
            return confirmedOrders.ToList();
        }
    }
}
