using adAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace adAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Advertisement> Advertisements { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            ModelBuilder.Entity<Advertisement>()
            .Property(b => b._Images).HasColumnName("Images");
        }
    }
}
