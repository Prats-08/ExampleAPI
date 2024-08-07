using Microsoft.EntityFrameworkCore.SqlServer;
using System.ComponentModel.DataAnnotations;
namespace ExampleAPI.Models
{
    public class EmployeeDetail
    {
        [Key]
        public int employeeId { get; set; }
        public string? employeeName { get; set; }
        public double employeeSalary { get; set; }
        public int employeeAge { get; set; }
        public int isActive {  get; set; }
    }
}
