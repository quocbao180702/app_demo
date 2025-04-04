using EmployeeManagement.Api;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace EmployeeManagement.Services.UserService
{
    public partial class UserService : IUserService
    {

        public async Task<BaseResponse> ChangePassword(UserRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var user = await _context.Users.FirstAsync(x => x.UserName == request.UserName);
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                if (!BC.Verify(request.Password, user.Password))
                {
                    response.Success = false;
                    response.Message = "Old password is incorrect";
                    return response;
                }
                user.Password = BC.HashPassword(request.Password);
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "Password changed successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while changing the password";
            }
            return response;
        }
    }
}
