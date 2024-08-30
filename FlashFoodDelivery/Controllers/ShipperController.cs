using Business_Layer.Repositories;
using Business_Layer.Services;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel;
using Data_Layer.ResourceModel.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSerivce _userSerivce;

        public ShipperController(IUserRepository userRepository, IUserSerivce userSerivce)
        {
            _userRepository = userRepository;
            _userSerivce = userSerivce;
        }

        [HttpGet]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllACcountShipper()
        {
            var result = await _userSerivce.GetShippersAsync();
            return Ok(result);
        }


        [HttpPost("register")]
        [EnableCors("CorsPolicy")]
        public async Task<APIResponseModel> RegisterShipper([FromBody] RegisterVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage).ToList();
                    return new APIResponseModel
                    {
                        code = 400,
                        Data = errors,
                        IsSuccess = false,
                        message = string.Join(";", errors)
                    };


                }

                var result = await _userRepository.RegisterShipper(model);
                return result;

            }
            catch (Exception ex)
            {
                return new APIResponseModel()
                {
                    code = StatusCodes.Status400BadRequest,
                    message = ex.Message,
                    Data = ex,
                    IsSuccess = false
                };
            }
        }

        [HttpPut("UpdateShipper")]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserUpdateViewModel model)
        {

            var user = await _userSerivce.UpdateShipper(id, model);
            if (!user.IsSuccess)
            {
                return BadRequest(user);
            }
            return Ok(user);

        }

        [HttpDelete("DeleteUser")]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteShipper(string id)
        {
            var deleteSuccess = await _userSerivce.DeleteShipper(id);
            if (!deleteSuccess.IsSuccess)
            {
                return BadRequest(deleteSuccess);
            }
            return Ok(deleteSuccess);
        }


    }
}
