using Business_Layer.Services;
using Data_Layer.ResourceModel.ViewModel.CartVMs;
using Data_Layer.ResourceModel.ViewModel.FeedBackVMs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FeedBacksController : ControllerBase
    {
        private readonly IFeedBackService _feedBackService; 

        public FeedBacksController(IFeedBackService feedBackService)
        {
            _feedBackService = feedBackService;
        }

        [HttpGet("{userId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllFeedBackByUserID(Guid userId)
        {
            var result = await _feedBackService.GetFeedBackByUserIDAsync(userId);
            return Ok(result);
        }

        [HttpGet("{Id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllFeedBackByID(Guid Id)
        {
            var result = await _feedBackService.GetFeedBackByIdAsync(Id);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllFeedBack()
        {
            var result = await _feedBackService.GetFeedBacksAsync();
            return Ok(result);
        }

        [HttpGet("{orderId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllFeedBackByOrderID(Guid orderId)
        {
            var result = await _feedBackService.GetFeedBackByOrderIDAsync(orderId);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateFeedBack([FromBody] FeedBackCreateVM createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }
            var c = await _feedBackService.CreateFeedBackAsync(createDto);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateFeedBack(Guid id, [FromBody] FeedBackUpdateVM updateDto)
        {
            var c = await _feedBackService.UpdateFeedBackAsync(id, updateDto);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteFeedBack(Guid id)
        {
            var c = await _feedBackService.DeleteFeedBackAsync(id);
            if (!c.IsSuccess)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }
    }
}
