using Business_Layer.Repositories;
using Business_Layer.Services;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.MenuFoodItemVMs;
using Data_Layer.ResourceModel.ViewModel.OrderVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuItemFoodController : ControllerBase
    {
        public IMenuFoodItemRepository _menuFoodItemRepository;
        public IMenuFoodItem1Service _menuFoodItem1Service;

        public MenuItemFoodController(IMenuFoodItemRepository menuFoodItemRepository, IMenuFoodItem1Service menuFoodItem1Service)
        {
            _menuFoodItemRepository = menuFoodItemRepository;
            _menuFoodItem1Service = menuFoodItem1Service;
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<APIResponseModel> GetAllMenuFoodItem()
        {
            try
            {
                var result = await _menuFoodItemRepository.GetMenuFoodItem();
                return new APIResponseModel()
                {
                    code = 200,
                    message = "Get successful",
                    IsSuccess = true,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllFoods()
        {
            var result = await _menuFoodItem1Service.GetFoodsAsync();
            return Ok(result);
        }

        [HttpGet("search")]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchFoods([FromQuery] string searchTerm)
        {
            var result = await _menuFoodItem1Service.SearchFoodsAsync(searchTerm);
            return Ok(result);
        }

        [HttpGet("{categoryId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllFoodByCategoryID(Guid categoryId)
        {
            var result = await _menuFoodItem1Service.GetFoodsByCategoryIdAsync(categoryId);
            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewFoodByID(Guid id)
        {
            var result = await _menuFoodItem1Service.GetFoodByIdsAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateFood([FromBody] MenuFoodItemCreateVM createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }
            var c = await _menuFoodItem1Service.CreateFoodAsync(createDto);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }

        [HttpPut("{id:Guid}")]
       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateFood(Guid id, [FromBody] MenuFoodItemUpdateVM updateDto)
        {
            var c = await _menuFoodItem1Service.UpdateFoodAsync(id, updateDto);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }


        [HttpDelete("{id:Guid}")]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteFood(Guid id)
        {
            var c = await _menuFoodItem1Service.DeleteFood(id);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }
    }
}
