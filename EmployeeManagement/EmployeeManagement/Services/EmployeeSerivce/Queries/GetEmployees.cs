using EmployeeManagement.Api;
using EmployeeManagement.Api.EmployeeResponse;
using EmployeeManagement.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EmployeeManagement.Services.EmployeeSerivce.cs
{
    public partial class EmployeeService : IEmployeeService
    {

        public async Task<BaseResponse> GetEmployees(TakeReponseEmployee request)
        {
            var response = new BaseResponse();
            try
            {
                var employeeQuery = _context.Employees.Include(x => x.Department).AsQueryable();
                if (!string.IsNullOrEmpty(request.Term))
                {
                    employeeQuery = employeeQuery.Where(x => x.TenNV.Contains(request.Term));
                }
                if (request.department.HasValue && request.department != 0)
                {
                    employeeQuery = employeeQuery.Where(x => x.MaBoPhan == request.department);
                }
                if (request.Sorter != null && !string.IsNullOrEmpty(request.Sorter.Order) && !string.IsNullOrEmpty(request.Sorter.Field))
                {
                    switch (request.Sorter.Field)
                    {
                        case "mucLuong":
                            employeeQuery = request.Sorter.Order == "desc" ? employeeQuery.OrderByDescending(x => x.MucLuong) : employeeQuery.OrderBy(x => x.MucLuong);
                            break;

                    }
                }
                else
                {
                    employeeQuery = employeeQuery.OrderBy(x => x.Id);
                }
                var totalRecords = employeeQuery.Count();
                employeeQuery = employeeQuery.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);
                var employees = await employeeQuery.ToListAsync();
                response.Success = true;
                response.Data = new
                {
                    /*Items = employees.Select(d => new EmployeeResponse
                    {
                        Id = d.Id,
                        MaNV = d.MaNV,
                        TenNV = d.TenNV,
                        GioiTinh = d.GioiTinh,
                        NgaySinh = d.NgaySinh,
                        MaBoPhan = d.MaBoPhan,
                        TenBoPhan = d.Department.Name,
                        MucLuong = d.MucLuong
                    }),*/
                    Items = _mapper.Map<List<EmployeeResponse>>(employees),
                    Metadata = new
                    {
                        request.Page,
                        request.PageSize,
                        totalRecords
                    }
                };

            }
            catch (Exception ex)
            {
                // Handle exception
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

    }
}
