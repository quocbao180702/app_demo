using EmployeeManagement.Api;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.Report
{
    public partial class Report : IReportService
    {
        //add method report total all employee
        public async Task<BaseResponse> ReportTotalEmployee()
        {
            var response = new BaseResponse();
            try
            {
                // Get total employee
                var totalEmployee = await _context.Employees.CountAsync();
                response.Success = true;
                response.Data = totalEmployee;
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
