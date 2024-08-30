using Business_Layer.Services;
using Data_Layer.ResourceModel.ViewModel.OrderDetailVMs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        public OrderDetailsController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllOrderDetails()
        {
            var result = await _orderDetailService.GetOrderDetailsAsync();
            return Ok(result);
        }

        [HttpGet("{orderId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllOrderDetailByOrderID(Guid orderId)
        {
            var result = await _orderDetailService.GetOrderDetailByOrderIdsAsync(orderId);
            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllOrderDetailByID(Guid orderDetailId)
        {
            var result = await _orderDetailService.GetOrderDetailByIdAsync(orderDetailId);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateOrderDetail([FromBody] OrderDetaiCreateVM createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }
            var c = await _orderDetailService.CreateOrderDetailAsync(createDto);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrderDetail(Guid id, [FromBody] OrderDetailUpdateVM updateDto)
        {
            var c = await _orderDetailService.UpdateOrderDetailAsync(id, updateDto);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletedSoftRangeOrderDetailByOrderID(Guid orderId)
        {
            var c = await _orderDetailService.DeletedOrderDetailRange(orderId);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }
    }
}
