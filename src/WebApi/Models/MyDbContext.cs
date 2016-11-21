using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
