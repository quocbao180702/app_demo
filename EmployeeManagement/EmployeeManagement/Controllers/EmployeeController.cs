using EmployeeManagement.Api;
using EmployeeManagement.Api.EmployeeRequest;
using EmployeeManagement.Services.EmployeeSerivce.cs;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        //call methods in EmployeeService
        [HttpPost("get-all-employees")]
        public async Task<BaseResponse> GetEmployees(TakeReponseEmployee request)
        {
            return await _employeeService.GetEmployees(request);
        }
        [HttpPost("get-employee-by-id")]
        public async Task<BaseResponse> GetEmployeeById(EmployeeRequest request)
        {
            return await _employeeService.GetEmployeeById(request.Id);
        }
        [HttpPost("create-employee")]
        public async Task<BaseResponse> CreateEmployee(EmployeeRequest request)
        {
            return await _employeeService.CreateEmployee(request);
        }
        [HttpPost("update-employee")]
        public async Task<BaseResponse> UpdateEmployee(EmployeeRequest request)
        {
            return await _employeeService.UpdateEmployee(request);
        }
        [HttpPost("delete-employee")]
        public async Task<BaseResponse> DeleteEmployee(EmployeeRequest request)
        {
            return await _employeeService.DeleteEmployee(request.Id);
        }
        //call ImportExcel method
        [HttpPost("import-excel")]
        public async Task<BaseResponse> ImportExcel([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return new BaseResponse { Success = false, Message = "No file uploaded" };
            }

            var filePath = Path.GetTempFileName();

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var importRequest = new EmployeeImport { FilePath = filePath };
            var response = await _employeeService.ImportExcel(importRequest);
            return response;
        }
    }
}
