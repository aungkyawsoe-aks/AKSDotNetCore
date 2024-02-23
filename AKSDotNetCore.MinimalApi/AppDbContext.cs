using AKSDotNetCore.MinimalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AKSDotNetCore.MinimalApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
