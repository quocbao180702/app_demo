using EmployeeManagement.Api;
using EmployeeManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.DepartmentService
{
    public partial class DepartmentService : IDepartmentService
    {
        //Add method CreateDepartment
        public async Task<BaseResponse> CreateDepartment(DepartmentRequest request)
        {
            var response = new BaseResponse();
            try
            {
                //check request
                if (request == null)
                {
                    response.Success = false;
                    response.Message = "Request is null";
                    return response;
                }
                //check parameter of request
                if (string.IsNullOrEmpty(request.MaBoPhan) || string.IsNullOrEmpty(request.TenBoPhan))
                {
                    response.Success = false;
                    response.Message = "MaBoPhan or TenBoPhan is null or empty";
                    return response;
                }
                var department = new Department
                {
                    MaBoPhan = request.MaBoPhan,
                    Name = request.TenBoPhan,
                };
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                response.Message = "Department created successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while creating the department";
            }
            return response;
        }
    }
}
