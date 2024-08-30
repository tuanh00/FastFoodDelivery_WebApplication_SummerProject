using AutoMapper;
using Business_Layer.DataAccess;
using Data_Layer.Models;
using Data_Layer.ResourceModel.ViewModel;
using Data_Layer.ResourceModel.ViewModel.MenuFoodItemVMs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repositories
{
    public class MenuItemFoodRepository : IMenuFoodItemRepository
    {
        private readonly FastFoodDeliveryDBContext _context;
        private readonly IMapper _mapper;

        public MenuItemFoodRepository(FastFoodDeliveryDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddProduct(MenuFoodItemVM menuFoodItemVM)
        {
            var menuFoodItem = _mapper.Map<MenuFoodItem>(menuFoodItemVM);
            _context.MenuFoodItems.Add(menuFoodItem);
            bool result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public async Task<List<MenuFoodItemVM>> GetMenuFoodItem()
        {
            var menuFoodItems = await _context.MenuFoodItems.ToListAsync();
            var result = _mapper.Map<List<MenuFoodItemVM>>(menuFoodItems);
            return result;
        }

        public async Task<MenuFoodItemVM> GetMenuFoodItemById(Guid Id)
        {
            var menuFoodItems = await _context.MenuFoodItems.FirstOrDefaultAsync(x => x.FoodId == Id);
            var result = _mapper.Map<MenuFoodItemVM>(menuFoodItems);
            return result;
        }        
    }
}
