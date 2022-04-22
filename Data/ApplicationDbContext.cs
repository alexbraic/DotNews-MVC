using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DotNews.Data;

namespace DotNews.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DotNews.Data.Report> Report { get; set; }
        public DbSet<DotNews.Data.Comment> Comment { get; set; }
    }
}