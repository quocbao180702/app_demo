using EmployeeManagement.Api;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.DepartmentService
{
    public partial class DepartmentService : IDepartmentService
    {
        //Add method GetDepartments
        public async Task<BaseResponse> GetDepartments()
        {
            var response = new BaseResponse();
            try
            {
                var departments = await _context.Departments.ToListAsync();
                response.Success = true;
                response.Data = departments;
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
