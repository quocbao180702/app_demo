using EmployeeManagement.Api;
using EmployeeManagement.Entities;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace EmployeeManagement.Services.UserService
{
    public interface IUserService
    {
        //call GetAllUsers method
        Task<BaseResponse> GetAllUsers();
        //call CreateUser method
        Task<BaseResponse> CreateUser(UserRequest request);
        //call other methods
        Task<BaseResponse> UpdateUser(UserRequest request);
        Task<BaseResponse> GetUser(int? id);
        Task<BaseResponse> DeleteUser(int? id);
    }
    public partial class UserService : IUserService
    {
        private readonly EmployeeDbContext _context;
        public UserService(EmployeeDbContext context)
        {
            _context = context;
        }





    }
}
