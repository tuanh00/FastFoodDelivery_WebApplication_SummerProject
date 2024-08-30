using Business_Layer.Repositories;
using Business_Layer.Services;
using Data_Layer.Models;
using Data_Layer.ResourceModel.Common;
using Data_Layer.ResourceModel.ViewModel.User;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSerivce _userSerivce;

        public UserController(IUserRepository userRepository, IUserSerivce userSerivce)
        {
            _userRepository = userRepository;
            _userSerivce = userSerivce;
        }
        [HttpGet("GetUserPagination")]
        public async Task<APIResponseModel> GetUserPagination(int pageIndex = 0, int pageSize = 10)
        {
            var users = await _userSerivce.GetUserPagingsionsAsync(pageIndex, pageSize);
            return new APIResponseModel()
            {
                code = 200,
                message = "List 10 User",
                IsSuccess = true,
                Data = users
            };
        }
        [HttpGet]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllACcountUser()
        {
            var result = await _userSerivce.GetUsersAsync();
            return Ok(result);
        }
        [HttpGet("GetUserById/{id}")]
        public async Task<APIResponseModel> GetUserById(string id)
        {
            try
            {
                var user = await _userSerivce.GetUserById(id);
                return new APIResponseModel
                {
                    code = 200,
                    IsSuccess = true,
                    Data = user,
                    message = "User Founded !",
                };
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
        [HttpPost("login")]
        [EnableCors("CorsPolicy")]
        public async Task<APIResponseModel> Login([FromBody] LoginVM model)
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

                var result = await _userRepository.Login(model);
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

        [HttpPost("register")]
        [EnableCors("CorsPolicy")]
        public async Task<APIResponseModel> Register([FromBody] RegisterVM model)
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

                var result = await _userRepository.Register(model);
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
        
        [HttpPut("UpdateUser")]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task <IActionResult> UpdateUser(string id, [FromBody] UserUpdateViewModel model)
        {
           
              var user = await _userSerivce.UpdateUser(id, model);
                if( !user.IsSuccess )
                {
                    return BadRequest(user);
                }
            return Ok(user);

        }
        [HttpDelete("DeleteUser")]
        [EnableCors("CorsPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var deleteSuccess = await _userSerivce.DeleteUser(id);
            if(!deleteSuccess.IsSuccess)
            {
                return BadRequest(deleteSuccess);
            }
            return Ok(deleteSuccess);
        }        
    }
}
