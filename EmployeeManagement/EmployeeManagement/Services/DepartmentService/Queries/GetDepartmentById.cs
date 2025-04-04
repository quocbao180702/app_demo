using EmployeeManagement.Api;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.DepartmentService
{
    public partial class DepartmentService : IDepartmentService
    {

        //Add method GetDepartmentById
        public async Task<BaseResponse> GetDepartmentById(int id)
        {
            var response = new BaseResponse();
            try
            {
                //check parameter of request
                if (id <= 0)
                {
                    response.Success = false;
                    response.Message = "Id is invalid";
                    return response;
                }
                // Get department by id
                var department = await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);
                if (department == null)
                {
                    response.Success = false;
                    response.Message = "Department not found";
                }
                response.Success = true;
                response.Data = department;
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
