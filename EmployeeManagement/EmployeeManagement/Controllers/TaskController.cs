using EmployeeManagement.Api;
using EmployeeManagement.Services.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet("get-all-tasks")]
        public async Task<BaseResponse> GetAllTasks()
        {
            return await _taskService.GetAllTasks();
        }
        [HttpPost("create-task")]
        public async Task<BaseResponse> CreateTask(TaskRequest request)
        {
            return await _taskService.CreateTask(request);
        }
        [HttpPost("update-task")]
        public async Task<BaseResponse> UpdateTask(TaskRequest request)
        {
            return await _taskService.UpdateTask(request);
        }
        [HttpPost("delete-task")]
        public async Task<BaseResponse> DeleteTask(TaskRequest request)
        {
            return await _taskService.DeleteTask(request);
        }
        [HttpPost("change-status-task")]
        public async Task<BaseResponse> ChangeStatusTask(TaskRequest request)
        {
            return await _taskService.ChangeStatusTask(request);
        }

    }
}
