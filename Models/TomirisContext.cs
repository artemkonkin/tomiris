using Microsoft.EntityFrameworkCore;

namespace tomiris.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BlogPostModel> BlogPosts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted(); //clear before create
            //Database.EnsureCreated();
        }
    }
}

// dotnet ef migrations add InitialCreate
// dotnet ef database update
// dotnet ef migrations list
// dotnet ef migrations remove
// 