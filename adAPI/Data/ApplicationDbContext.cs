using adAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace adAPI.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Advertisement> Advertisements { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
    }
}
