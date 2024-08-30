using Business_Layer.Commons;
using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Utils
{
    public static class UserViewModelExtensions
    {
        public static UserViewModel ToUserViewModel(this User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Status = user.Status,
            };
        }
        public static User ToUser(this UserViewModel model)
        {
            return new User
            {
                FullName = model.FullName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email
                
            };
        }
        public static Pagination<UserViewModel> ToUserViewModel(this Pagination<User> pagination)
        {
            return new Pagination<UserViewModel>
            {
                TotalItemsCount = pagination.TotalItemsCount,
                PageSize = pagination.PageSize,
                PageIndex = pagination.PageIndex,
                Items = pagination.Items.Select(t => t.ToUserViewModel()).ToList()
            };
        }
    }
}
