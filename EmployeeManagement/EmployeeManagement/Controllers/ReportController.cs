using EmployeeManagement.Api;
using EmployeeManagement.Services.Report;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }


        [HttpPost("report-total-employee")]
        //call ReportTotalEmployee method
        public async Task<BaseResponse> ReportTotalEmployee()
        {
            return await _reportService.ReportTotalEmployee();
        }
        [HttpPost("report-total-employee-by-department")]
        //call ReportTotalEmployeeByDepartment method
        public async Task<BaseResponse> ReportTotalEmployeeByDepartment()
        {
            return await _reportService.ReportTotalEmployeeByDepartment();
        }
        [HttpPost("average-salary-per-department")]
        public async Task<BaseResponse> AverageSalaryPerDepartment()
        {
            return await _reportService.AverageSalaryPerDepartment();
        }

        [HttpPost("top-10-highest-paid-employees")]
        public async Task<BaseResponse> Top10HighestPaidEmployees()
        {
            return await _reportService.Top10HighestPaidEmployees();
        }

    }
}
