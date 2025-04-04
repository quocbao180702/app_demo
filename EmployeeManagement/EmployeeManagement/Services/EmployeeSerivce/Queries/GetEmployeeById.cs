using EmployeeManagement.Api;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.EmployeeSerivce.cs
{
    public partial class EmployeeService : IEmployeeService
    {
        // Add method GetEmployeeById
        public async Task<BaseResponse> GetEmployeeById(int? id)
        {
            var response = new BaseResponse();
            try
            {
                if (id == null)
                {
                    response.Success = false;
                    response.Message = "Id không hợp lệ";
                }
                // Get employee by id
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
                if (employee == null)
                {
                    response.Success = false;
                    response.Message = "Không tìm thấy nhân viên";
                }
                else
                {
                    response.Success = true;
                    response.Data = employee;
                }
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
