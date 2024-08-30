using AutoMapper;
using Business_Layer.Repositories;
using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel;
using Data_Layer.ResourceModel.ViewModel.Enum;
using Data_Layer.ResourceModel.ViewModel.MenuFoodItemVMs;
using Data_Layer.ResourceModel.ViewModel.OrderDetailVMs;
using Data_Layer.ResourceModel.ViewModel.OrderVMs;

namespace Business_Layer.Services
{
    public class MenuFoodItem1Service : IMenuFoodItem1Service
    {
        private readonly IMenuFoodItem1Repository _menuFoodItem1Repository;
        private readonly IMapper _mapper;
        public MenuFoodItem1Service(IMenuFoodItem1Repository menuFoodItem1Repository, IMapper mapper)
        {
            _menuFoodItem1Repository = menuFoodItem1Repository;
            _mapper = mapper;
        }
        public async Task<APIResponseModel> CreateFoodAsync(MenuFoodItemCreateVM createdto)
        {
            APIResponseModel reponse = new APIResponseModel();
            try
            {
                var Entity = _mapper.Map<MenuFoodItem>(createdto);
                Entity.FoodStatus = "Active";
                await _menuFoodItem1Repository.AddAsync(Entity);
                if (await _menuFoodItem1Repository.SaveAsync() > 0)
                {
                    reponse.Data = _mapper.Map<MenuFoodItemViewVM>(Entity);
                    reponse.IsSuccess = true;
                    reponse.message = "Create new Food Item successfully";
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = ex.Message;
            }
            return reponse;
        }

        public async Task<APIResponseModel> DeleteFood(Guid id)
        {
            var reponse = new APIResponseModel();
            try
            {
                var foodChecked = await _menuFoodItem1Repository.GetByIdAsync(id);

                if (foodChecked == null)
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Not found food, you are sure input";
                }
                else if (foodChecked.FoodStatus == "IsDeleted")
                {
                    reponse.IsSuccess = false;
                    reponse.message = "food is deleted, can not delete food again.";
                }
                else
                {

                    foodChecked.FoodStatus = "IsDeleted";
                    var foodFofUpdate = _mapper.Map<MenuFoodItemViewVM>(foodChecked);
                    var foodDTOAfterUpdate = _mapper.Map<MenuFoodItemViewVM>(foodFofUpdate);
                    if (await _menuFoodItem1Repository.SaveAsync() > 0)
                    {
                        reponse.Data = foodDTOAfterUpdate;
                        reponse.IsSuccess = true;
                        reponse.message = "Update food successfully";
                    }
                    else
                    {
                        reponse.Data = foodDTOAfterUpdate;
                        reponse.IsSuccess = false;
                        reponse.message = "Update food fail!";
                    }
                }
            }
            catch (Exception e)
            {
                reponse.IsSuccess = false;
                reponse.message = $"Delete food fail!, exception {e.Message}";
            }

            return reponse;
        }

        public async Task<APIResponseModel> GetFoodByIdsAsync(Guid foodId)
        {
            var _response = new APIResponseModel();
            try
            {
                var c = await _menuFoodItem1Repository.GetByIdAsync(foodId, x => x.Category);
                if (c == null)
                {
                    _response.IsSuccess = false;
                    _response.message = "Don't Have Any Food ";
                }
                else
                {
                    var EnityDTO = _mapper.Map<MenuFoodItemViewVM>(c);
                    EnityDTO.CategoryName = c.Category.CategoriesName;
                    _response.Data = EnityDTO;
                    _response.IsSuccess = true;
                    _response.message = "Food Retrieved Successfully";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.message = ex.Message;
            }

            return _response;
        }

        public async Task<APIResponseModel> GetFoodsAsync()
        {
            var reponse = new APIResponseModel();
            List<MenuFoodItemViewVM> FoodDTOs = new List<MenuFoodItemViewVM>();
            try
            {
                var foods = await _menuFoodItem1Repository.GetAllAsync(x => x.Category);
                foreach (var food in foods)
                {
                    if(food.FoodStatus.ToString() == MenuFoodItemStatusEnum.Active.ToString())
                    {
                        var EnityDTO = _mapper.Map<MenuFoodItemViewVM>(food);
                        EnityDTO.CategoryName = food.Category.CategoriesName;
                        FoodDTOs.Add(EnityDTO);
                    }
                    
                }
                if (FoodDTOs.Count > 0)
                {
                    reponse.Data = FoodDTOs;
                    reponse.IsSuccess = true;
                    reponse.message = $"Have {FoodDTOs.Count} food.";
                    return reponse;
                }
                else
                {
                    reponse.IsSuccess = false;
                    reponse.message = $"Have {FoodDTOs.Count} food.";
                    return reponse;
                }
            }
            catch (Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = ex.Message;
                return reponse;
            }

        }

        public async Task<APIResponseModel> GetFoodsByCategoryIdAsync(Guid categoryId)
        {
            var reponse = new APIResponseModel();
            List<MenuFoodItemViewVM> FoodDTOs = new List<MenuFoodItemViewVM>();
            try
            {
                List<MenuFoodItem> foodAll = await _menuFoodItem1Repository.GetAllAsync( x => x.Category);
                if(foodAll != null)
                {
                    var fillterFoods = foodAll.Where(x => x.CategoryId == categoryId).ToList();
                    if (fillterFoods.Any())
                    {
                        foreach (var food in fillterFoods)
                        {
                            var EnityDTO = _mapper.Map<MenuFoodItemViewVM>(food);
                            EnityDTO.CategoryName = food.Category.CategoriesName;
                            FoodDTOs.Add(EnityDTO);
                        }
                        if (FoodDTOs.Count > 0)
                        {
                            reponse.Data = FoodDTOs;
                            reponse.IsSuccess = true;
                            reponse.message = $"Have {FoodDTOs.Count} food.";
                            return reponse;
                        }
                        else
                        {
                            reponse.IsSuccess = false;
                            reponse.message = $"Have {FoodDTOs.Count} food. Food is null, not found";
                            return reponse;
                        }
                    }
                    else
                    {
                        reponse.IsSuccess = false;
                        reponse.message = "Not found any food";
                        return reponse;
                    }
                }
                else
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Food Is Null";
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

        public async Task<APIResponseModel> SearchFoodsAsync(string searchTerm)
        {
            var response = new APIResponseModel();
            List<MenuFoodItemViewVM> foodDTOs = new List<MenuFoodItemViewVM>();
            try
            {
                var foods = await _menuFoodItem1Repository.GetAllAsync(x => x.Category);

                // Apply search filter if searchTerm is provided
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    foods = foods.Where(f => f.FoodName.ToLower().Contains(searchTerm) || f.FoodDescription.ToLower().Contains(searchTerm)).ToList();
                }

                foreach (var food in foods)
                {
                    if (food.FoodStatus.ToString() == MenuFoodItemStatusEnum.Active.ToString())
                    {
                        var entityDTO = _mapper.Map<MenuFoodItemViewVM>(food);
                        entityDTO.CategoryName = food.Category.CategoriesName;
                        foodDTOs.Add(entityDTO);
                    }
                }

                if (foodDTOs.Count > 0)
                {
                    response.Data = foodDTOs;
                    response.IsSuccess = true;
                    response.message = $"Found {foodDTOs.Count} food items.";
                }
                else
                {
                    response.Data = foodDTOs; // Returning empty list to maintain consistency
                    response.IsSuccess = false;
                    response.message = "No food items found.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.message = ex.Message;
            }

            return response;
        }

        public async Task<APIResponseModel> UpdateFoodAsync(Guid id, MenuFoodItemUpdateVM updatedto)
        {
            var reponse = new APIResponseModel();
            try
            {
                var foodChecked = await _menuFoodItem1Repository.GetByIdAsync(id);

                if (foodChecked == null || foodChecked.FoodStatus == "IsDeleted")
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Not found food, you are sure input";
                }
                else
                {
                    var foodFofUpdate = _mapper.Map(updatedto, foodChecked);
                    var foodDTOAfterUpdate = _mapper.Map<MenuFoodItemViewVM>(foodFofUpdate);
                    if (await _menuFoodItem1Repository.SaveAsync() > 0)
                    {
                        reponse.Data = foodDTOAfterUpdate;
                        reponse.IsSuccess = true;
                        reponse.message = "Update food successfully";
                    }
                    else
                    {
                     
                        reponse.IsSuccess = false;
                        reponse.message = "Update food fail!";
                    }
                }
            }
            catch (Exception e)
            {
                reponse.IsSuccess = false;
                reponse.message = $"Update food fail!, exception {e.Message}";
            }

            return reponse;
        }        
    }
}
