using EmployeeManagement.Api;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.Tasks
{
    public partial class TaskService : ITaskService
    {
        public async Task<BaseResponse> CreateTask(TaskRequest task)
        {
            var response = new BaseResponse();
            try
            {
                if (task == null)
                {
                    response.Success = false;
                    response.Message = "Không có dữ liệu công việc";
                    return response;
                }
                var newTask = new Entities.Tasks();
                newTask.Name = task.Name;
                newTask.Description = task.Description;
                newTask.StartDate = task.StartDate.Value.ToLocalTime();
                newTask.EndDate = task.EndDate.Value.ToLocalTime();
                newTask.Status = task.Status;
                newTask.Assign = task.EmployeeId;
                newTask.CreatedAt = DateTime.Now.ToLocalTime();
                newTask.UpdatedAt = DateTime.Now.ToLocalTime();

                _context.Tasks.Add(newTask);
                await _context.SaveChangesAsync();

                var tasks =  _context.Tasks.Include(x => x.Employees).FirstOrDefault(x => x.Id == newTask.Id);
                response.Data = _mapper.Map<TaskResponse>(tasks);
                response.Success = true;
                response.Message = "Thêm công việc thành công";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while creating the task: " + ex.Message;
            }
            return response;
        }
    }
}
