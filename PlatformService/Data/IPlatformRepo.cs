using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
        bool SaveChange();
        IEnumerable<Platform> GetAllFlatform();
        Platform GetFlatformById(int id);
        void CreateFlatform(Platform model);
    }
}
