using Microsoft.EntityFrameworkCore;
using NUCCITWebApp.Models;

namespace NUCCITWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } // Existing DbSet for Users

        public DbSet<Comment> Comments { get; set; } // New DbSet for Comments
    }
}
