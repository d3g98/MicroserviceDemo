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

        public void CreateFlatform(Platform model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _dbContext.Flatforms.Add(model);
        }

        public IEnumerable<Platform> GetAllFlatform()
        {
            return _dbContext.Flatforms.OrderBy(x => x.Name).ToList();
        }

        public Platform GetFlatformById(int id)
        {
            return _dbContext.Flatforms.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChange()
        {
            return _dbContext.SaveChanges() > 0;
        }
    }
}
