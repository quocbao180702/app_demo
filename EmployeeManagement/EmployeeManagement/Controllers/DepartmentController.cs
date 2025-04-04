using EmployeeManagement.Api;
using EmployeeManagement.Services.DepartmentService;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet("get-department")]
        //call GetDepartments method
        public async Task<BaseResponse> GetDepartments()
        {
            return await _departmentService.GetDepartments();
        }
        [HttpGet]
        [Route("{id}")]
        //call GetDepartmentById method
        public async Task<BaseResponse> GetDepartmentById(int id)
        {
            return await _departmentService.GetDepartmentById(id);
        }
        [HttpPost("create-department")]
        //call CreateDepartment method
        public async Task<BaseResponse> CreateDepartment(DepartmentRequest request)
        {
            return await _departmentService.CreateDepartment(request);
        }
        [HttpPut]
        //call UpdateDepartment method
        public async Task<BaseResponse> UpdateDepartment(DepartmentRequest request)
        {
            return await _departmentService.UpdateDepartment(request);
        }
        [HttpDelete]
        [Route("{id}")]
        //call DeleteDepartment method
        public async Task<BaseResponse> DeleteDepartment(int id)
        {
            return await _departmentService.DeleteDepartment(id);
        }
    }
}
