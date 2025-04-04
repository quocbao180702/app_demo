using EmployeeManagement.Api;
using EmployeeManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.DepartmentService
{
    //add interface IDepartmentService
    public interface IDepartmentService
    {
        //add method GetDepartments
        Task<BaseResponse> GetDepartments();
        //add method GetDepartmentById
        Task<BaseResponse> GetDepartmentById(int id);
        //add method CreateDepartment
        Task<BaseResponse> CreateDepartment(DepartmentRequest request);
        //add method UpdateDepartment
        Task<BaseResponse> UpdateDepartment(DepartmentRequest request);
        //add method DeleteDepartment
        Task<BaseResponse> DeleteDepartment(int id);
    }
    public partial class DepartmentService : IDepartmentService
    {
        private readonly EmployeeDbContext _context;
        public DepartmentService(EmployeeDbContext context)
        {
            _context = context;
        }




    }
}
