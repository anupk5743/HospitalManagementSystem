
using Microsoft.EntityFrameworkCore;

namespace HMS.Api
{
    public class DepartmentDbContext : DbContext
    {
        public DepartmentDbContext(DbContextOptions<DepartmentDbContext> options)
            : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; };

        // Example placeholder for entity sets - uncomment and replace with real entity types:
        // public DbSet<Department> Departments { get; set; } = null!;
    }
}