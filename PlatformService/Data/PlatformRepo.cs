using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _dbContext;
        public PlatformRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreatePlatform(Platform model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _dbContext.Platforms.Add(model);
        }

        public IEnumerable<Platform> GetAllPlatform()
        {
            return _dbContext.Platforms.OrderBy(x => x.Name).ToList();
        }

        public Platform GetPlatformById(int id)
        {
            return _dbContext.Platforms.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChange()
        {
            return _dbContext.SaveChanges() > 0;
        }
    }
}