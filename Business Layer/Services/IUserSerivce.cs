using Business_Layer.Commons;
using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public interface IUserSerivce
    {
        Task<APIResponseModel> GetUsersAsync();
        Task<APIResponseModel> GetShippersAsync();
        Task<APIResponseModel> UpdateShipper(string id, UserUpdateViewModel model);
        Task<APIResponseModel> DeleteShipper(string id);
        Task<UserViewModel> GetUserById(string id);
        Task<APIResponseModel> UpdateUser(string id, UserUpdateViewModel model);
        Task<APIResponseModel> DeleteUser (string id);
        Task<Pagination<UserViewModel>> GetUserPagingsionsAsync(int pageIndex = 0, int pageSize = 10);
    }
}
