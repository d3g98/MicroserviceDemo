using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class DbExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>().HasData(
                new Platform() { Id = 1, Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                new Platform() { Id = 2, Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                new Platform() { Id = 3, Name = "Kubernetes", Publisher = "Cloud Native Conputing Foundation", Cost = "Free" }
                );
        }

        public static void AutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
