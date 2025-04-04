using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Entities
{
    [Table("Tasks")]
    public class Tasks
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; } = true;
        public int? Assign { get; set; }
        [ForeignKey("Assign")]
        public virtual Employees? Employees { get; set; }
        [Column(TypeName = "timestamp without time zone")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "timestamp without time zone")]

        public DateTime? EndDate { get; set; }
        [Column(TypeName = "timestamp without time zone")]

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column(TypeName = "timestamp without time zone")]

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
