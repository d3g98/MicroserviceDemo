using CommandService.Data;
using CommandService.Models;
using CommandService.SyncDataServices.Grpc;

namespace PlatformService.Data
{
    public static class DbExtentions
    {
        public static void AutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void Seed(this WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();

                var platforms = grpcClient.ReturnAllPlatforms();

                InsertData(serviceScope.ServiceProvider.GetService<ICommandRepo>(), platforms);
            }
        }

        private static void InsertData(ICommandRepo? commandRepo, IEnumerable<Platform> platforms)
        {
            if (platforms != null)
            {
                Console.WriteLine("--> Seeding new platforms...");

                foreach (var plat in platforms)
                {
                    if (!commandRepo.ExternalPlatformExists(plat.ExternalId))
                    {
                        commandRepo.CreatePlatform(plat);
                    }
                    commandRepo.SaveChanges();
                }
            }
            else
            {
                Console.WriteLine("--> Not exists data.");
            }
        }
    }
}
