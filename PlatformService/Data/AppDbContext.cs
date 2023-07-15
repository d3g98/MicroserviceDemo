using PlatformService.Models;
using Microsoft.EntityFrameworkCore;

namespace PlatformService.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;
        public AppDbContext(IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string nameDb = "PlatformConns";
            if (_environment.IsProduction())
            {
                options.UseSqlServer(_configuration.GetConnectionString(nameDb));
            }
            else
            {
                options.UseInMemoryDatabase(nameDb);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>().ToTable("tblPlatform");
            modelBuilder.Entity<Platform>().HasKey(x => x.Id);
            modelBuilder.Entity<Platform>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Platform>().Property(x => x.Publisher).IsRequired();
            modelBuilder.Entity<Platform>().Property(x => x.Cost).IsRequired();

            modelBuilder.Seed();
        }

        public DbSet<Platform> Platforms { get; set; }
    }
}