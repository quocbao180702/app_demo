using EmployeeManagement.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Api
{
    public class EmployeeImport
    {
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public bool? GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? MaBoPhan { get; set; }
        public double? MucLuong { get; set; }
        public bool? IsValid { get; set; }
        public string? Message { get; set; }
        public string? FilePath { get; set; }
    }
}
