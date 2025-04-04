using EmployeeManagement.Api;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.EmployeeSerivce.cs
{
    public partial  class EmployeeService : IEmployeeService
    {


        // Add method DeleteEmployee
        public async Task<BaseResponse> DeleteEmployee(int? id)
        {
            var response = new BaseResponse();
            try
            {
                // Get employee by id
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
                if (employee == null)
                {
                    response.Success = false;
                    response.Message = "Không tìm thấy nhân viên";
                }
                else
                {
                    // Delete employee
                    _context.Employees.Remove(employee);
                    await _context.SaveChangesAsync();
                    response.Success = true;
                    response.Message = "Xóa nhân viên thành công";
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
