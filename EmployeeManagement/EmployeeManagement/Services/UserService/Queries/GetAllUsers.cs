using EmployeeManagement.Api;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.UserService
{
    public partial class UserService : IUserService
    {
        public async Task<BaseResponse> GetAllUsers()
        {
            var response = new BaseResponse();
            try
            {
                // Get all users
                var users = await _context.Users.ToListAsync();
                response.Success = true;
                response.Data = users;
            }
            catch (Exception ex)
            {
                // Handle exception
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

    }
}
