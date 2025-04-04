using EmployeeManagement.Api;
using EmployeeManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.Report
{
    public interface IReportService
    {
        //add method report total all employee
        Task<BaseResponse> ReportTotalEmployee();
        //add method report total employee by department
        Task<BaseResponse> ReportTotalEmployeeByDepartment();

        //add method Average salary per department
        Task<BaseResponse> AverageSalaryPerDepartment();

        //add method Top 10 highest-paid employees
        Task<BaseResponse> Top10HighestPaidEmployees();

    }
    public partial class Report : IReportService
    {
        private readonly EmployeeDbContext _context;
        public Report(EmployeeDbContext context)
        {
            _context = context;
        }

    }

}
