using CommandService.Models;
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
            string nameDb = "CommandConns";
            //if (_environment.IsProduction())
            //{
            //    options.UseSqlServer(_configuration.GetConnectionString(nameDb));
            //}
            //else
            //{
            //    options.UseInMemoryDatabase(nameDb);
            //}
            options.UseInMemoryDatabase(nameDb);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Platform
            modelBuilder.Entity<Platform>().ToTable("tblPlatform");
            modelBuilder.Entity<Platform>().HasKey(x => x.Id);
            modelBuilder.Entity<Platform>().Property(x => x.ExternalId).IsRequired();
            modelBuilder.Entity<Platform>().Property(x => x.Name).IsRequired();

            //Command
            modelBuilder.Entity<Command>().ToTable("tblCommand");
            modelBuilder.Entity<Command>().HasKey(x => x.Id);
            modelBuilder.Entity<Command>().Property(x => x.CommandLine).IsRequired();
            modelBuilder.Entity<Command>().Property(x => x.PlatformId).IsRequired();

            //Relations
            modelBuilder
                .Entity<Platform>()
                .HasMany(p => p.Commands)
                .WithOne(p => p.Platform!)
                .HasForeignKey(p => p.PlatformId);
            modelBuilder
                .Entity<Command>()
                .HasOne(p => p.Platform)
                .WithMany(p => p.Commands)
                .HasForeignKey(p => p.PlatformId);
        }

        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Command> Commands { get; set; }
    }
}