using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.FeedBackVMs;
using Data_Layer.ResourceModel.ViewModel.OrderDetailVMs;
using Data_Layer.ResourceModel.ViewModel.OrderVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public interface IFeedBackService 
    {
        Task<APIResponseModel> GetFeedBacksAsync();
        Task<APIResponseModel> GetFeedBackByIdAsync(Guid Id);
        Task<APIResponseModel> GetFeedBackByUserIDAsync(Guid userId);
        Task<APIResponseModel> GetFeedBackByOrderIDAsync(Guid orderID);
        Task<APIResponseModel> CreateFeedBackAsync(FeedBackCreateVM createdto);
        Task<APIResponseModel> UpdateFeedBackAsync(Guid id, FeedBackUpdateVM updatedto);
        Task<APIResponseModel> DeleteFeedBackAsync(Guid id);
        Task<APIResponseModel> GetSortedFeedBacksAsync(string sortName);
    }
}
