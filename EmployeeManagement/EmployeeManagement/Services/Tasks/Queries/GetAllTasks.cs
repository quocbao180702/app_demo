using EmployeeManagement.Api;
using EmployeeManagement.Api.EmployeeResponse;
using EmployeeManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services.Tasks
{
    public partial class TaskService : ITaskService
    {
        // create a method to get all tasks
        public async Task<BaseResponse> GetAllTasks()
        {
            var response = new BaseResponse();
            try
            {
                var taskquery  =  _context.Tasks.Include(x => x.Employees).AsQueryable();
                var tasks = await taskquery.OrderBy(x => x.Id).ToListAsync(); ;
                if (tasks == null)
                {
                    response.Success = false;
                    response.Message = "Không tìm thấy task";
                }

                response.Data = _mapper.Map<List<TaskResponse>>(tasks);
                response.Success = true;
                response.Message = "Lấy dữ liệu thành công";
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while retrieving tasks: " + ex.Message;
            }
            return response;

        }
    }
}
