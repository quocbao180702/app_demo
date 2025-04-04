using EmployeeManagement.Api;

namespace EmployeeManagement.Services.Tasks
{
    public partial class TaskService : ITaskService
    {
        // add a method to change the status of a task
        public async Task<BaseResponse> ChangeStatusTask(TaskRequest task)
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
                taskEntity.Status = task.Status;
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "Thay đổi trạng thái công việc thành công";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while changing the status of the task: " + ex.Message;
            }
            return response;
        }
    }
}
