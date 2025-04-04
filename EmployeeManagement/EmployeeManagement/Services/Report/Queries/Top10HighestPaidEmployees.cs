using EmployeeManagement.Api;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.Report
{
    public partial class Report : IReportService
    {

        //Add method Top10HighestPaidEmployees
        public async Task<BaseResponse> Top10HighestPaidEmployees()
        {
            var response = new BaseResponse();
            try
            {
                // Get top 10 highest-paid employees
                var employees = await _context.Employees.OrderByDescending(x => x.MucLuong).Take(10).ToListAsync();
                response.Success = true;
                response.Data = employees;
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
