using Microsoft.EntityFrameworkCore;

namespace tomiris.Models
{
    public class TomirisContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public TomirisContext(DbContextOptions<TomirisContext> options)
            : base(options)
        {
            Database.EnsureDeleted(); //clear before create
            Database.EnsureCreated();
        }
    }
}