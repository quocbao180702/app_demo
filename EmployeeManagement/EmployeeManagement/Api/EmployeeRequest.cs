namespace EmployeeManagement.Api.EmployeeRequest
{
    public class EmployeeRequest
    {
        public int? Id { get; set; }
        public string? MaNV { get; set; }
        public string? TenNV { get; set; }
        public bool? GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public int? MaBoPhan { get; set; }
        public double? MucLuong { get; set; }
    }
}
