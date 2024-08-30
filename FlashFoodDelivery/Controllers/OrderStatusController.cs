using Business_Layer.Services;
using Data_Layer.ResourceModel.ViewModel;
using Data_Layer.ResourceModel.ViewModel.MenuFoodItemVMs;
using Data_Layer.ResourceModel.ViewModel.OrderStatusVMs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly IOrderStatusService _orderStatusService;

        public OrderStatusController(IOrderStatusService orderStatusService)
        {
            _orderStatusService = orderStatusService;
        }
        [HttpGet]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllOrderStatus()
        {
            var result = await _orderStatusService.GetOrderStatusAsync();
            return Ok(result);
        }

        [HttpPost]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateOrderStatus([FromBody] OrderStatusVM orderStatusVM)
        {
            if (orderStatusVM == null)
            {
                return BadRequest();
            }
            var c = await _orderStatusService.CreateOrderStatusAsync(orderStatusVM);
            if (!c.IsSuccess)
            {
                return BadRequest();
            }

            return Ok(c);
        }
        [HttpPut("{id:Guid}")]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateFood(Guid id, [FromBody] OrderStatusUpdateVM updateDto)
        {
            var c = await _orderStatusService.UpdateOrderStatusAsync(id, updateDto);
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
            var c = await _orderStatusService.DeleteOrderStatus(id);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }
    }
}
