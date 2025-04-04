using EmployeeManagement.Api;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.DepartmentService
{
    public partial class DepartmentService : IDepartmentService
    {

        //Add method DeleteDepartment
        public async Task<BaseResponse> DeleteDepartment(int id)
        {
            var response = new BaseResponse();
            try
            {
                var department = await _context.Departments.FirstAsync(x => x.Id == id);
                if (department == null)
                {
                    response.Success = false;
                    response.Message = "Department not found";
                }
                else
                {
                    _context.Departments.Remove(department);
                    await _context.SaveChangesAsync();
                    response.Message = "Department deleted successfully";
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while deleting the department";
            }
            return response;
        }
    }
}
