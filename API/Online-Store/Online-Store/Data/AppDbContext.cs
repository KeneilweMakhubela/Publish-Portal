using Microsoft.EntityFrameworkCore;
using Online_Store.Models.Domain;

namespace Online_Store.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<BlogPost> BlogPostC { get; set; }
        public DbSet<Category> CategoryC  { get; set; }
    }
}
