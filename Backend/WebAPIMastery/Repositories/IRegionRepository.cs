using WebAPIMastery.Models.Domain;

namespace WebAPIMastery.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegions();
        Task<Region> CreateNewRegion(Region region);
        Task<Region?> IsRegionExist(string code);
        Task<Region> ModifyRegion(string code, Region region);
        Task<Region?> DeleteRegion(string code);
        Task<Person> AddPersonDetail(Person person);
    }
}
