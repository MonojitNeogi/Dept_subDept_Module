using Dept_subDept_Module.Models;
using Microsoft.EntityFrameworkCore;

namespace Dept_subDept_Module.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Reminder> Reminder { get; set; }
    }
}
