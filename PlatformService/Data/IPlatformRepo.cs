using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
        bool SaveChange();
        IEnumerable<Platform> GetAllPlatform();
        Platform GetPlatformById(int id);
        void CreatePlatform(Platform model);
    }
}
