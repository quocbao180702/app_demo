using EmployeeManagement.Api;

namespace EmployeeManagement.Services.UserService
{
    public partial class UserService : IUserService
    {

        public async Task<BaseResponse> GetUser(int? id)
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
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                }
                response.Success = true;
                response.Data = new UserResponse
                {
                    Name = user.Name,
                    Email = user.Email,
                    UserName = user.UserName
                };

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while getting the user";
            }
            return response;
        }
    }
}
