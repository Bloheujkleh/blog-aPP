using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Data
{
    public class AppDbContext: IdentityDbContext
    {   public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) 
        {

        }
        public DbSet<Post> Posts { get; set; }
    }
}
