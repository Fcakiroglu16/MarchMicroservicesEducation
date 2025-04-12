using Microsoft.EntityFrameworkCore;

namespace DockerExample.API.Models
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; } = null!;
    }
}
