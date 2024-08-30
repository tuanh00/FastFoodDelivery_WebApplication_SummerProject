using AutoMapper;
using Business_Layer.Repositories;
using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel;
using Data_Layer.ResourceModel.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<APIResponseModel> CreateCategoryAsync(CategoryCreateVM category)
        {
            APIResponseModel responseModel = new APIResponseModel();
            try
            {
                var Entity = _mapper.Map<Category>(category);
                Entity.CategoriesStatus = "Active";
                await _categoryRepository.AddAsync(Entity);
                if(await _categoryRepository.SaveAsync() > 0)
                {
                    responseModel.Data = _mapper.Map<CategoryVM>(Entity);
                    responseModel.code = 200;
                    responseModel.IsSuccess = true;
                    responseModel.message = "Create new Category Successfully";
                }
            }
            catch (Exception ex)
            {
                responseModel.code=500;
                responseModel.IsSuccess=false;
                responseModel.message=ex.Message;
            }

            return responseModel;
        }

        public async Task<APIResponseModel> DeleteCategory(Guid id)
        {
            var reponse = new APIResponseModel();
            try
            {
                var CategoryChecked = await _categoryRepository.GetByIdAsync(id);

                if (CategoryChecked == null)
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Not found Category, you are sure Input";
                }
                else if(CategoryChecked.CategoriesStatus == "IsDeleted")
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Category is Deleted, Not Deleted Again.";
                }
                else
                {
                    CategoryChecked.CategoriesStatus = "IsDeleted";
                    var CategoryUpdateStatus = _mapper.Map<CategoryVM>(CategoryChecked);
                    var CategoryDTOAfterUpdate = _mapper.Map<CategoryVM>(CategoryUpdateStatus);
                    if(await _categoryRepository.SaveAsync() > 0)
                    {

                        reponse.Data = CategoryDTOAfterUpdate;
                        reponse.code = 200;
                        reponse.IsSuccess = true;
                        reponse.message = "Delete Category Successfully";
                    }
                    else
                    {
                        reponse.Data = CategoryDTOAfterUpdate;
                        reponse.IsSuccess = false;
                        reponse.message = "Delete category fail!";
                    }
                }
            }catch (Exception ex)
            {
                reponse.IsSuccess = false;
                reponse.message = $"Delete category Fail!, exception {ex.Message}";
            }
            return reponse;
        }

        public async Task<APIResponseModel> GetCategoryAsync()
        {
            var reponse = new APIResponseModel();
            List<CategoryVM> CategoryDTO = new List<CategoryVM>();
            try
            {
                var categorys = await _categoryRepository.GetCategoryAll();
                   
                foreach(var category in categorys)
                {
                    CategoryDTO.Add(_mapper.Map<CategoryVM>(category));
                }
                if(CategoryDTO.Count > 0)
                {
                    reponse.Data = CategoryDTO;
                    reponse.code = 200;
                    reponse.IsSuccess = true;
                    reponse.message = $"Have {CategoryDTO.Count} food.";
                    return reponse;
                }
                else
                {
                    reponse.IsSuccess = false;
                    reponse.message = $"Have {CategoryDTO.Count} food.";
                    return reponse;
                }
            }catch(Exception ex)
            {

                reponse.IsSuccess = false;
                reponse.message = ex.Message;
                return reponse;
            }
        }

        public async Task<APIResponseModel> GetCategoryByIdsAsync(Guid categoryId)
        {
           var _response = new APIResponseModel();
            try
            {
                var c = await _categoryRepository.GetByIdAsync(categoryId);
                if(c == null)
                {
                    _response.IsSuccess = false;
                    _response.message = "Don't Have Any Category";
                }
                else
                {
                    _response.Data = _mapper.Map<CategoryVM>(c);
                    _response.code = 200;
                    _response.IsSuccess = true;
                    _response.message = "find Category Successfully";
                }
            }catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.message = ex.Message;
            }
            return _response;
        }

        public async Task<APIResponseModel> UpdateCategoryAsync(Guid id, CategoryUpdateVM category)
        {
            var reponse = new APIResponseModel();
            try
            {
                var categoryChecked = await _categoryRepository.GetByIdAsync(id);
                if(categoryChecked == null || categoryChecked.CategoriesStatus == "IsDeleted")
                {
                    reponse.IsSuccess = false;
                    reponse.message = "Not fond food, you are sure input";
                }
                else
                {
                    var categoryofUpdate = _mapper.Map(category, categoryChecked);
                    var categoryDTOAfterUpdate = _mapper.Map<CategoryVM>(categoryofUpdate);
                    if(await _categoryRepository.SaveAsync() > 0)
                    {
                        reponse.Data = categoryDTOAfterUpdate;
                        reponse.code = 200;
                        reponse.IsSuccess = true;
                        reponse.message = "Update Category successfully";
                    }
                    else
                    {
                        reponse.Data = categoryDTOAfterUpdate;
                        reponse.IsSuccess = false;
                        reponse.message = "Update food fail!";
                    }
                }
            }
            catch(Exception ex)
            {
                reponse.IsSuccess=false;
                reponse.message = $"Update category fail!, exception {ex.Message}";
            }
            return reponse;
        }
    }
}
