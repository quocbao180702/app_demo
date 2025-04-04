using EmployeeManagement.Api.EmployeeRequest;
using EmployeeManagement.Api;
using EmployeeManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.EmployeeSerivce.cs
{
    public partial class EmployeeService : IEmployeeService
    {


        // Add method CreateEmployee
        public async Task<BaseResponse> CreateEmployee(EmployeeRequest employee)
        {
            var response = new BaseResponse();
            try
            {
                // Check if employee already exists
                var existingEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);
                if (existingEmployee != null)
                {
                    response.Success = false;
                    response.Message = "Nhân viên đã tồn tại";
                    return response;
                }
                var newEmployee = new Employees
                {
                    TenNV = employee.TenNV,
                    MaNV = employee.MaNV,
                    MaBoPhan = employee.MaBoPhan,
                    NgaySinh = employee.NgaySinh.HasValue ? DateTime.SpecifyKind(employee.NgaySinh.Value, DateTimeKind.Utc) : null, // Ensure UTC
                    GioiTinh = employee.GioiTinh,
                    MucLuong = employee.MucLuong ?? 0,
                };
                // Add new employee
                _context.Employees.Add(newEmployee);
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "Thêm nhân viên thành công";
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
