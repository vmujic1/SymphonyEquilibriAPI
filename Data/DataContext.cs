using SymphonyEquilibriAPI.Models.Employee;
using SymphonyEquilibriAPI.Models.Project;
using SymphonyEquilibriAPI.Models.Statistics;
using SymphonyEquilibriAPI.Models.User;

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

        public DbSet<User> Users { get; set; }
    }
}
