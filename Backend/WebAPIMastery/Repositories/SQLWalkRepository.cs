using Microsoft.EntityFrameworkCore;
using WebAPIMastery.Data;
using WebAPIMastery.Models.Domain;

namespace WebAPIMastery.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateWalks(Walk walk)
        {
            var walkExist = await dbContext.Walks.FirstOrDefaultAsync(x => x.Name == walk.Name);

            if(walkExist == null)
            {
                var addRegion = new Region
                {
                    Id = Guid.NewGuid(),
                    Code = walk.Region.Code,
                    Name = walk.Region.Name,
                    RegionImageUrl = walk.Region.RegionImageUrl
                };

                var addDifficulty = new Difficulty
                {
                    Id = Guid.NewGuid(),
                    Name = walk.Difficulty.Name
                };

                var addWalks = new Walk
                {
                    Id = Guid.NewGuid(),
                    Name = walk.Name,
                    Description = walk.Description,
                    LengthInKm = walk.LengthInKm,
                    WalkImageUrl = walk.WalkImageUrl,
                    DifficultyId = addDifficulty.Id,
                    RegionId = addRegion.Id
                };

                await dbContext.AddAsync(addRegion);
                await dbContext.AddAsync(addDifficulty);
                await dbContext.AddAsync(addWalks);

                await dbContext.SaveChangesAsync();

                return addWalks;
            }

            return null;
        }

        public async Task<List<Walk>> DisplayWalk()
        {
            var displayWalk = await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            return displayWalk;
        }

        public async Task<List<Walk>> DisplayWalkByName(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true)
        {
            var walk = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if(filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walk = walk.Where(x => x.Name.Contains(filterQuery));
                }
            
            }

            //Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walk = isAscending ? walk.OrderBy(x => x.Name) : walk.OrderByDescending(x => x.Name);
                }

                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase)) 
                {
                    walk = isAscending ? walk.OrderBy(x => x.LengthInKm) : walk.OrderByDescending(x => x.LengthInKm);
                }
            }

            var displayWalk = await walk.ToListAsync();
            return displayWalk;
        }
    }
}
