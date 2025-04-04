using EmployeeManagement.Api.EmployeeRequest;
using EmployeeManagement.Api;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.EmployeeSerivce.cs
{
    public partial class EmployeeService : IEmployeeService
    {

        // Add method UpdateEmployee
        public async Task<BaseResponse> UpdateEmployee(EmployeeRequest employee)
        {
            var response = new BaseResponse();
            try
            {
                // Update employee
                //_context.Employees.Update(employee);
                var existingEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);
                if (existingEmployee == null)
                {
                    response.Success = false;
                    response.Message = "Không tìm thấy nhân viên";
                    return response;
                }
                existingEmployee.TenNV = employee.TenNV;
                existingEmployee.MaNV = employee.MaNV;
                existingEmployee.MaBoPhan = employee.MaBoPhan;
                existingEmployee.NgaySinh = employee.NgaySinh.HasValue ? DateTime.SpecifyKind(employee.NgaySinh.Value, DateTimeKind.Utc) : null;
                existingEmployee.GioiTinh = employee.GioiTinh;
                existingEmployee.MucLuong = employee.MucLuong ?? 0;
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "Cập nhật nhân viên thành công";
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
