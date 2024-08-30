using AutoMapper;
using Business_Layer.Repositories;
using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.FeedBackVMs;
using Data_Layer.ResourceModel.ViewModel.OrderVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class FeedBackService : IFeedBackService
    {
        private readonly IFeedBackRepository _feedBackRepository;
        private readonly IMapper _mapper;

        public FeedBackService(IFeedBackRepository feedBackRepository, IMapper mapper)
        {
            _feedBackRepository = feedBackRepository;
            _mapper = mapper;
        }

        public async Task<APIResponseModel> CreateFeedBackAsync(FeedBackCreateVM createdto)
        {
            var reponse = new APIResponseModel();

            try
            {
                var entity = _mapper.Map<FeedBack>(createdto);
                await _feedBackRepository.AddAsync(entity);

                if (await _feedBackRepository.SaveAsync() > 0)
                {
                    reponse.Data = _mapper.Map<FeedBackViewVM>(entity);
                    reponse.IsSuccess = true;
                    reponse.message = "Create new feedback successfully";
                    return reponse;
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = ex.Message;
                return reponse;
            }

            return reponse;
        }

        public async Task<APIResponseModel> DeleteFeedBackAsync(Guid id)
        {
            var reponse = new APIResponseModel();
            try
            {
                var Checked = await _feedBackRepository.GetByIdAsync(id);

                if (Checked == null)
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Not found feedback, you are sure input";
                }
                else
                {
                    _feedBackRepository.Remove(Checked);
                    if (await _feedBackRepository.SaveAsync() > 0)
                    {
                        reponse.IsSuccess = true;
                        reponse.message = "Deleted feedback successfully";
                    }
                    else
                    {
                        reponse.IsSuccess = false;
                        reponse.message = "Update feedback fail!";
                    }
                }
            }
            catch (Exception e)
            {
                reponse.IsSuccess = false;
                reponse.message = $"Update feedback fail!, exception {e.Message}";
            }

            return reponse;
        }

        public async Task<APIResponseModel> GetFeedBackByIdAsync(Guid Id)
        {
            var _response = new APIResponseModel();
            try
            {
                var c = await _feedBackRepository.GetByIdAsync(Id);
                if (c == null)
                {
                    _response.IsSuccess = false;
                    _response.message = "Don't Have Any FeedBack ";
                }
                else
                {
                    var mapper = _mapper.Map<FeedBackViewVM>(c);
                    mapper.UserName = c.User.FullName;
                    _response.Data = mapper;
                    _response.IsSuccess = true;
                    _response.message = "FeedBack Retrieved Successfully";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.message = ex.Message;
            }

            return _response;
        }

        public async Task<APIResponseModel> GetFeedBackByOrderIDAsync(Guid orderID)
        {
            var reponse = new APIResponseModel();
            List<FeedBackViewVM> DTOs = new List<FeedBackViewVM>();
            try
            {
                List<FeedBack> feedbacks = (await _feedBackRepository.GetAllAsync(x => x.User)).Where(x => x.OrderId == orderID).ToList();
                foreach (var f in feedbacks)
                {
                    var mapper = _mapper.Map<FeedBackViewVM>(f);
                    mapper.UserName = f.User.FullName;
                    DTOs.Add(mapper);
                }
                if (DTOs.Count > 0)
                {
                    reponse.Data = DTOs;
                    reponse.IsSuccess = true;
                    reponse.message = $"Have {DTOs.Count} feedback.";
                    return reponse;
                }
                else
                {
                    reponse.IsSuccess = false;
                    reponse.message = $"Have {DTOs.Count} feedback. feedback is null, not found";
                    return reponse;
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = "Exception";
                return reponse;
            }
        }

        public async Task<APIResponseModel> GetFeedBackByUserIDAsync(Guid userId)
        {
            var reponse = new APIResponseModel();
            List<FeedBackViewVM> DTOs = new List<FeedBackViewVM>();
            try
            {
                List<FeedBack> feedbacks = (await _feedBackRepository.GetAllAsync(x => x.User)).Where(x => x.UserId == userId.ToString()).ToList();
                foreach (var f in feedbacks)
                {
                    var mapper = _mapper.Map<FeedBackViewVM>(f);
                    mapper.UserName = f.User.FullName;
                    DTOs.Add(mapper);
                }
                if (DTOs.Count > 0)
                {
                    reponse.Data = DTOs;
                    reponse.IsSuccess = true;
                    reponse.message = $"Have {DTOs.Count} feedback.";
                    return reponse;
                }
                else
                {
                    reponse.IsSuccess = false;
                    reponse.message = $"Have {DTOs.Count} feedback. feedback is null, not found";
                    return reponse;
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = "Exception";
                return reponse;
            }
        }

        public async Task<APIResponseModel> GetFeedBacksAsync()
        {
            var reponse = new APIResponseModel();
            List<FeedBackViewVM> DTOs = new List<FeedBackViewVM>();
            try
            {
                List<FeedBack> feedbacks = (await _feedBackRepository.GetAllAsync(x => x.User)).ToList();
                foreach (var f in feedbacks)
                {
                    var mapper = _mapper.Map<FeedBackViewVM>(f);
                    mapper.UserName = f.User.FullName;
                    DTOs.Add(mapper);
                }
                if (DTOs.Count > 0)
                {
                    reponse.Data = DTOs;
                    reponse.IsSuccess = true;
                    reponse.message = $"Have {DTOs.Count} feedback.";
                    return reponse;
                }
                else
                {
                    reponse.IsSuccess = false;
                    reponse.message = $"Have {DTOs.Count} feedback. feedback is null, not found";
                    return reponse;
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = "Exception";
                return reponse;
            }
        }

        public Task<APIResponseModel> GetSortedFeedBacksAsync(string sortName)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponseModel> UpdateFeedBackAsync(Guid id, FeedBackUpdateVM updatedto)
        {
            var reponse = new APIResponseModel();
            try
            {
                var Checked = await _feedBackRepository.GetByIdAsync(id);

                if (Checked == null)
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Not found feedback, you are sure input";
                }
                else
                {
                    var FofUpdate = _mapper.Map(updatedto, Checked);
                    var DTOAfterUpdate = _mapper.Map<FeedBackViewVM>(FofUpdate);
                    if (await _feedBackRepository.SaveAsync() > 0)
                    {
                        reponse.Data = DTOAfterUpdate;
                        reponse.IsSuccess = true;
                        reponse.message = "Update feedback successfully";
                    }
                    else
                    {
                        reponse.Data = DTOAfterUpdate;
                        reponse.IsSuccess = false;
                        reponse.message = "Update feedback fail!";
                    }
                }
            }
            catch (Exception e)
            {
                reponse.IsSuccess = false;
                reponse.message = $"Update feedback fail!, exception {e.Message}";
            }

            return reponse;
        }
    }
}
