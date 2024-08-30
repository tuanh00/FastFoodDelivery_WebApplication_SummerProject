using Business_Layer.Services;
using Data_Layer.ResourceModel.ViewModel.CartVMs;
using Data_Layer.ResourceModel.ViewModel.OrderVMs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Climate;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllOrderByUserID(Guid userId)
        {
            var result = await _cartService.GetCartsByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddToCart([FromBody] CartCreateVM createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }
            var c = await _cartService.CreateCartAsync(createDto);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] CartUpdateVM updateDto)
        {
            var c = await _cartService.UpdateQuanityCartAsync(id, updateDto);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCart(Guid id)
        {
            var c = await _cartService.DeleteCartAsync(id);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }
    }
}
