using EmployeeManagement.Api;

namespace EmployeeManagement.Services.Tasks
{
    public partial class TaskService : ITaskService
    {
        public async Task<BaseResponse> DeleteTask(TaskRequest task)
        {
            var response = new BaseResponse();
            try
            {
                var taskEntity =  _context.Tasks.FirstOrDefault(x => x.Id == task.Id);
                if (taskEntity == null)
                {
                    response.Success = false;
                    response.Message = "Không tìm thấy công việc";
                    return response;
                }
                _context.Tasks.Remove(taskEntity);
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "Xóa công việc thành công";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while deleting the task: " + ex.Message;
            }
            return response;
        }
    }
}
