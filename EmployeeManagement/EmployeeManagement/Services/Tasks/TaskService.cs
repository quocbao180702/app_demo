using AutoMapper;
using EmployeeManagement.Api;
using EmployeeManagement.Entities;

namespace EmployeeManagement.Services.Tasks
{
    public interface ITaskService
    {
        Task<BaseResponse> GetAllTasks();
        Task<BaseResponse> CreateTask(TaskRequest task);
        Task<BaseResponse> UpdateTask(TaskRequest task);
        Task<BaseResponse> DeleteTask(TaskRequest task);
        Task<BaseResponse> ChangeStatusTask(TaskRequest task);
    }
    public partial class TaskService : ITaskService
    {
        private readonly EmployeeDbContext _context;
        private readonly IMapper _mapper;

        public TaskService(EmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
