using Business_Layer.Repositories;
using Business_Layer.Services;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel;
using Data_Layer.ResourceModel.ViewModel.Category;
using Data_Layer.ResourceModel.ViewModel.MenuFoodItemVMs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }



        //private readonly ICategoryRepository _repository;

        //public CategoryController(ICategoryRepository repository)
        //{
        //    _repository = repository;
        //}

        //[HttpGet("list")]
        //public async Task<APIResponseModel> GetAllCategories()
        //{
        //    var data = await _repository.GetAllCategory();
        //    try
        //    {
        //        return new APIResponseModel()
        //        {
        //            code = 200,
        //            message = "Get All Category successful",
        //            IsSuccess = true,
        //            Data = data,
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new APIResponseModel()
        //        {
        //            code = 400,
        //            IsSuccess = false,
        //            message = ex.Message,
        //        };
        //    }
        //}

        [HttpGet]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllCategorys()
        {
            var result = await _categoryService.GetCategoryAsync();
            return Ok(result);
        }

        [HttpPost]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateVM category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            var c = await _categoryService.CreateCategoryAsync(category);
            if(!c.IsSuccess)
            {
                return BadRequest();
            }

            return Ok(c);
        }

        [HttpDelete("{id:Guid}")]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var c = await _categoryService.DeleteCategory(id);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryUpdateVM category)
        {
            var c = await _categoryService.UpdateCategoryAsync(id, category);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }
    }
}
