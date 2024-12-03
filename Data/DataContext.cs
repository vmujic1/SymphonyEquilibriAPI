using SymphonyEquilibriAPI.Models.Employee;
using SymphonyEquilibriAPI.Models.Project;
using SymphonyEquilibriAPI.Models.Statistics;

namespace SymphonyEquilibriAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Statistics> Statistics { get; set; }
    }
}
