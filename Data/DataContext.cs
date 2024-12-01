using Microsoft.EntityFrameworkCore;

namespace SymphonyEquilibriAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
