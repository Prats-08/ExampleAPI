using Microsoft.EntityFrameworkCore;

namespace ExampleAPI.Models
{
    public class DetailDbContext : DbContext
    {
        public DetailDbContext(DbContextOptions<DetailDbContext> options) :base(options)
        {
            
        }
        public DbSet<EmployeeDetail> Employees { get; set; }
        public DbSet<LoginDetail> LoginDetails { get; set; }
    }
}
