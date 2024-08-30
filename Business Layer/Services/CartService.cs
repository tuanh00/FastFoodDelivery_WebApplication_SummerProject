using AutoMapper;
using Business_Layer.Repositories;
using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.CartVMs;
using Data_Layer.ResourceModel.ViewModel.OrderVMs;

namespace Business_Layer.Services
{
    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        public CartService(IMapper mapper, ICartRepository cartRepository)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
        }
        public async Task<APIResponseModel> CreateCartAsync(CartCreateVM createdto)
        {
            var reponse = new APIResponseModel();

            try
            {
                var checkcart = await _cartRepository.GetAllAsync();
                var Checkedfilter = checkcart.Where(x => x.UserID == createdto.UserID && x.foodId == createdto.foodId).FirstOrDefault();
                if (Checkedfilter != null)
                {
                    Checkedfilter.Quantity = Checkedfilter.Quantity++;
                     _cartRepository.Update(Checkedfilter);
                    if (await _cartRepository.SaveAsync() > 0)
                    {
                        reponse.Data = _mapper.Map<CartViewVM>(Checkedfilter);
                        reponse.IsSuccess = true;
                        reponse.message = "add more food for cart successfully";
                        return reponse;
                    }
                }
                else
                {
                    var entity = _mapper.Map<Cart>(createdto);
                    await _cartRepository.AddAsync(entity);
                    if (await _cartRepository.SaveAsync() > 0)
                    {
                        reponse.Data = _mapper.Map<CartViewVM>(entity);
                        reponse.IsSuccess = true;
                        reponse.message = "Create new cart successfully";
                        return reponse;
                    }
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

        public async Task<APIResponseModel> DeleteCartAsync(Guid id)
        {
            var reponse = new APIResponseModel();
            try
            {
                var Checked = await _cartRepository.GetByIdAsync(id);

                if (Checked == null)
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Not found cart, you are sure input";
                }
                else
                {

                    var DTOAfterUpdate = _mapper.Map<CartViewVM>(Checked);
                    _cartRepository.Remove(Checked);
                    if (await _cartRepository.SaveAsync() > 0)
                    {
                        reponse.Data = DTOAfterUpdate;
                        reponse.IsSuccess = true;
                        reponse.message = "Update cart successfully";
                    }
                    else
                    {
                        reponse.Data = DTOAfterUpdate;
                        reponse.IsSuccess = false;
                        reponse.message = "Update cart fail!";
                    }
                }
            }
            catch (Exception e)
            {
                reponse.IsSuccess = false;
                reponse.message = $"Update cart fail!, exception {e.Message}";
            }

            return reponse;
        }

        public async Task<APIResponseModel> GetCartsByUserIdAsync(Guid userID)
        {
            var reponse = new APIResponseModel();
            List<CartViewVM> DTOs = new List<CartViewVM>();
            try
            {
                var entityChecked = await _cartRepository.GetAllAsync();
                List<Cart> carts = entityChecked.Where(x => x.UserID == userID.ToString()).ToList();
                foreach (var cart in carts)
                {
                    DTOs.Add(_mapper.Map<CartViewVM>(cart));
                }
                if (DTOs.Count > 0)
                {
                    reponse.Data = DTOs;
                    reponse.IsSuccess = true;
                    reponse.message = $"Have {DTOs.Count} cart.";
                    return reponse;
                }
                else
                {
                    reponse.IsSuccess = false;
                    reponse.message = $"Have {DTOs.Count} cart. Cart is null, not found";
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

        public async Task<APIResponseModel> UpdateQuanityCartAsync(Guid id, CartUpdateVM updatedto)
        {
            var reponse = new APIResponseModel();
            try
            {
                var Checked = await _cartRepository.GetByIdAsync(id);

                if (Checked == null)
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Not found cart, you are sure input";
                }
                else
                {
                    if(Checked.Quantity <=0)
                    {
                        _cartRepository.Remove(Checked);
                        if (await _cartRepository.SaveAsync() > 0)
                        {
                            reponse.IsSuccess = true;
                            reponse.message = "Remove cart successfully, Because quantity equal 0";
                        }
                    }
                    else
                    {
                        var FofUpdate = _mapper.Map(updatedto, Checked);
                        var DTOAfterUpdate = _mapper.Map<CartViewVM>(FofUpdate);
                        if (await _cartRepository.SaveAsync() > 0)
                        {
                            reponse.Data = DTOAfterUpdate;
                            reponse.IsSuccess = true;
                            reponse.message = "Update cart successfully";
                        }
                        else
                        {
                            reponse.Data = DTOAfterUpdate;
                            reponse.IsSuccess = false;
                            reponse.message = "Update cart fail!";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                reponse.IsSuccess = false;
                reponse.message = $"Update cart fail!, exception {e.Message}";
            }

            return reponse;
        }
    }
}
