using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.ComponentModel.DataAnnotations;

namespace ExampleAPI.Models
{
    [Index(nameof(loginName), IsUnique = true)]
    public class LoginDetail
    {
        [Key]
        public int adminId { get; set; }
        [Required]
        public string? loginName { get; set; }
        [Required]
        public string? password { get; set; }
        [Required]
        public int employeeId { get; set; }
    }
}
