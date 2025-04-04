using EmployeeManagement.Api;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.Report
{
    public partial class Report : IReportService
    {        //Add method ReportTotalEmployeeByDepartment
        public async Task<BaseResponse> ReportTotalEmployeeByDepartment()
        {
            var response = new BaseResponse();
            try
            {
                // Get total employee by department
                var departments = await _context.Departments.ToListAsync();
                var report = new List<object>();
                foreach (var department in departments)
                {
                    var totalEmployee = await _context.Employees.CountAsync(x => x.MaBoPhan == department.Id);
                    report.Add(new
                    {
                        DepartmentName = department.Name,
                        TotalEmployee = totalEmployee
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
