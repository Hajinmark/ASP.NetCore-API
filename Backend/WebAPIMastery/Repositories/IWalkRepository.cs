using WebAPIMastery.Models.Domain;

namespace WebAPIMastery.Repositories
{
    public interface IWalkRepository
    {
        //Task<List<Walk>> GetAllWalks();
        Task<Walk> CreateWalks(Walk walk);
        Task<List<Walk>> DisplayWalk();
        Task<List<Walk>> DisplayAllWalkAsync();
        Task<List<Walk>> DisplayWalkByName(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
    }
}
