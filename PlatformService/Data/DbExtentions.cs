using PlatformService.Models;

namespace PlatformService.Data
{
    public static class DbExtentions
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var dbContext = provider.GetRequiredService<AppDbContext>())
                {
                    List<Platform> lst = new List<Platform>
                            {
                                new Platform() { Id = 1, Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                                new Platform() { Id = 2, Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                                new Platform() { Id = 3, Name = "Kubernetes", Publisher = "Cloud Native Conputing Foundation", Cost = "Free" }
                            };
                    dbContext.AddRange(lst);
                    dbContext.SaveChanges();
                }
            }
        }

        public static void AutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
