using EmployeeManagement.Api;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.DepartmentService
{
    public partial class DepartmentService : IDepartmentService
    {
        //Add method UpdateDepartment
        public async Task<BaseResponse> UpdateDepartment(DepartmentRequest request)
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
                //check department
                var department = await _context.Departments.FirstAsync(x => x.Id == request.Id);
                if (department == null)
                {
                    response.Success = false;
                    response.Message = "Department not found";
                }
                else
                {
                    department.MaBoPhan = request.MaBoPhan;
                    department.Name = request.TenBoPhan;
                    await _context.SaveChangesAsync();
                    response.Message = "Department updated successfully";
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while updating the department";
            }
            return response;
        }
    }
}
