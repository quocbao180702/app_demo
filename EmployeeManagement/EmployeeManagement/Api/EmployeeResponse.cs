namespace EmployeeManagement.Api.EmployeeResponse
{
    public class EmployeeResponse
    {
        public int? Id { get; set; }
        public string? MaNV { get; set; }
        public string? TenNV { get; set; }
        public bool? GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public int? MaBoPhan { get; set; }
        public string? TenBoPhan { get; set; }
        public double? MucLuong { get; set; }
    }
}
