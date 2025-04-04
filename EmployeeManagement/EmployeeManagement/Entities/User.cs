using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Entities
{
    [Table("Users")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        
        public string? Name { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? Password { get; set; }
        public string? UserName { get; set; }
    }
}
