using AutoMapper;
using EmployeeManagement.Api;
using EmployeeManagement.Api.EmployeeResponse;
using EmployeeManagement.Entities;

namespace EmployeeManagement.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentResponse>();
            CreateMap<User, UserResponse>();
            CreateMap<Employees, EmployeeResponse>()
                .ForMember(d => d.TenBoPhan, o => o.MapFrom(s => s.Department.Name));
            CreateMap<Employees, ReportResponse>();
            CreateMap<Tasks, TaskResponse>()
                .ForMember(d => d.EmployeeName, o => o.MapFrom(s => s.Employees.TenNV))
                .ForMember(d => d.EmployeeId, o => o.MapFrom(s => s.Employees.Id));


        }
    }
}
