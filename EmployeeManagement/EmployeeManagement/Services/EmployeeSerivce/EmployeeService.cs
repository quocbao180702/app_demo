using AutoMapper;
using EmployeeManagement.Api;
using EmployeeManagement.Api.EmployeeRequest;
using EmployeeManagement.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeManagement.Services.EmployeeSerivce.cs
{
    public interface IEmployeeService
    {
        //call GetEmployees method
        Task<BaseResponse> GetEmployees(TakeReponseEmployee request);
        //call GetEmployeeById method
        Task<BaseResponse> GetEmployeeById(int? id);
        //call CreateEmployee method
        Task<BaseResponse> CreateEmployee(EmployeeRequest employee);
        //call UpdateEmployee method
        Task<BaseResponse> UpdateEmployee(EmployeeRequest employee);
        //call DeleteEmployee method
        Task<BaseResponse> DeleteEmployee(int? id);
        Task<BaseResponse> ImportExcel(EmployeeImport request);

    }
    public partial class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDbContext _context;
        private readonly IMapper _mapper;
        public EmployeeService(EmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
