using EmployeeManagement.Api;

namespace EmployeeManagement.Services.Tasks
{
    public partial class TaskService : ITaskService
    {
        // create a method to update task
        public async Task<BaseResponse> UpdateTask(TaskRequest task)
        {
            var response = new BaseResponse();
            try
            {
                var taskEntity = _context.Tasks.FirstOrDefault(x => x.Id == task.Id);
                if (taskEntity == null)
                {
                    response.Success = false;
                    response.Message = "Không tìm thấy task";
                    return response;
                }
                taskEntity.Name = task.Name;
                taskEntity.Description = task.Description;
                taskEntity.StartDate = task.StartDate.Value.ToLocalTime();
                taskEntity.EndDate = task.EndDate.Value.ToLocalTime();
                taskEntity.Status = task.Status;
                taskEntity.Assign = task.EmployeeId;
                taskEntity.UpdatedAt = DateTime.Now.ToLocalTime();
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "Cập nhật công việc thành công";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while updating the task: " + ex.Message;
            }
            return response;
        }
    }
}
