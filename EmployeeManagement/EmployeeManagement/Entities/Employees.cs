using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Entities
{
    [Table("Employees")]
    public class Employees
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public bool? GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public int? MaBoPhan { get; set; }
        [ForeignKey("MaBoPhan")]
        public virtual Department? Department { get; set; }
        public double? MucLuong { get; set; }

        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
