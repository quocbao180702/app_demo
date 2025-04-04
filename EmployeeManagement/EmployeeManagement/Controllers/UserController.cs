using EmployeeManagement.Api;
using EmployeeManagement.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpPost("create-user")]
        public async Task<BaseResponse> CreateUser(UserRequest request)
        {
            return await _userService.CreateUser(request);
        }

        [HttpGet("get-all-users")]
        public async Task<BaseResponse> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpPut("update-user")]
        public async Task<BaseResponse> UpdateUser(UserRequest request)
        {
            return await _userService.UpdateUser(request);
        }

        [HttpPost("get-user")]
        public async Task<BaseResponse> GetUser(UserRequest request)
        {
            return await _userService.GetUser(request.Id);
        }

        [HttpPost("delete-user")]
        public async Task<BaseResponse> DeleteUser(UserRequest request)
        {
            return await _userService.DeleteUser(request.Id);
        }
    }
}
