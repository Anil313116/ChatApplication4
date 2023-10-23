using Microsoft.EntityFrameworkCore;

namespace ChatApplication4.Models
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CustomUser> CustomUsers { get; set; }
    }
}
