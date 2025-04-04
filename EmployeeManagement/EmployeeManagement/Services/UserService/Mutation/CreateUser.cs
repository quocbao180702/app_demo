using EmployeeManagement.Api;
using EmployeeManagement.Entities;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace EmployeeManagement.Services.UserService
{
    public partial class UserService : IUserService
    {
        public async Task<BaseResponse> CreateUser(UserRequest request)
        {
            var response = new BaseResponse();
            try
            {
                if (_context.Users.Any(x => x.UserName == request.UserName))
                {
                    response.Success = false;
                    response.Message = "User already exists";
                    return response;
                }
                var user = new User() { };
                user.Email = request.Email;
                user.Name = request.Name;
                user.UserName = request.UserName;
                user.Password = BC.HashPassword(request.Password);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                response.Message = "User created successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while creating the user";
            }
            return response;
        }
    }
}
