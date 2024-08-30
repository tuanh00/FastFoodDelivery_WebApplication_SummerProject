using Business_Layer.DataAccess;
using Data_Layer.Models;

namespace Business_Layer.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly FastFoodDeliveryDBContext _dbContext;
        public CartRepository(FastFoodDeliveryDBContext context, FastFoodDeliveryDBContext dbContext) : base(context)
        {
            _dbContext = dbContext;
        }
    }
}
