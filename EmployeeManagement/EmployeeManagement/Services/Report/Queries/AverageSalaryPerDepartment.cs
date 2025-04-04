using EmployeeManagement.Api;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.Report
{
    public partial class Report : IReportService
    {
        //Add method AverageSalaryPerDepartment
        public async Task<BaseResponse> AverageSalaryPerDepartment()
        {
            var response = new BaseResponse();
            try
            {
                // Get average salary per department
                var departments = await _context.Departments.ToListAsync();
                var report = new List<object>();
                foreach (var department in departments)
                {
                    var totalEmployee = await _context.Employees.CountAsync(x => x.MaBoPhan == department.Id);
                    var totalSalary = await _context.Employees.Where(x => x.MaBoPhan == department.Id).SumAsync(x => x.MucLuong);
                    var averageSalary = totalSalary / totalEmployee;
                    report.Add(new
                    {
                        DepartmentName = department.Name,
                        AverageSalary = averageSalary
                    });
                }
                response.Success = true;
                response.Data = report;
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
