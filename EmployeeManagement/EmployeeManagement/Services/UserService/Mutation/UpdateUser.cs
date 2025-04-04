using EmployeeManagement.Api;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.UserService
{
    public partial class UserService : IUserService
    {
        public async Task<BaseResponse> UpdateUser(UserRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.UserName == request.UserName);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                user.Name = request.Name;
                user.Email = request.Email;
                user.UserName = request.UserName;
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "User updated successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while updating the user";
            }
            return response;
        }
    }
}
