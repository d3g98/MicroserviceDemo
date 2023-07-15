using PlatformService.Models;
using Microsoft.EntityFrameworkCore;

namespace PlatformService.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("DbMicroService");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>().ToTable("tblFlatform");
            modelBuilder.Entity<Platform>().HasKey(x => x.Id);
            modelBuilder.Entity<Platform>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Platform>().Property(x => x.Publisher).IsRequired();
            modelBuilder.Entity<Platform>().Property(x => x.Cost).IsRequired();
        }

        public DbSet<Platform> Flatforms { get; set; }
    }
}