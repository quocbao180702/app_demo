using EmployeeManagement.Api;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.UserService
{
    public partial class UserService : IUserService
    {
        public async Task<BaseResponse> DeleteUser(int? id)
        {
            var response = new BaseResponse();
            try
            {
                if (id == null || id == 0)
                {
                    response.Success = false;
                    response.Message = "Id is invalid";
                    return response;
                }
                var user = _context.Users.FirstOrDefault(x => x.Id == id);
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "User deleted successfully";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while deleting the user";
            }
            return response;
        }
    }
}
