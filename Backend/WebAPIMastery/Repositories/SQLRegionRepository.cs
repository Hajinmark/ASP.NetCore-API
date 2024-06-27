using Microsoft.EntityFrameworkCore;
using WebAPIMastery.Data;
using WebAPIMastery.Models.Domain;

namespace WebAPIMastery.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLRegionRepository(NZWalksDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<Person> AddPersonDetail(Person person)
        {
            var isPersonExist = await dbContext.Persons.FirstOrDefaultAsync(x => x.Id == person.Id);

            if(isPersonExist == null)
            {
                var addPerson = new Person
                {
                    Id = Guid.NewGuid(),
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Age = person.Age,
                    Gender = person.Gender,
                };

                var addDetail = new Detail
                {
                    Id= Guid.NewGuid(),
                    PersonId = addPerson.Id,
                    Address = person.Detail.Address,
                    City = person.Detail.City
                };

                await dbContext.AddAsync(addPerson);
                await dbContext.AddAsync(addDetail);

                await dbContext.SaveChangesAsync(); 

                return person;

            }

            return person;
        }

        public async Task<Region> CreateNewRegion(Region region)
        {
            var isRegionExist = await dbContext.Regions.FirstOrDefaultAsync(x => x.Code == region.Code);
           
            if (isRegionExist == null)
            {
                var addRegion = new Region
                {
                    Id = Guid.NewGuid(),
                    Code = region.Code.ToUpper(),
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                };

                await dbContext.Regions.AddAsync(addRegion);
                await dbContext.SaveChangesAsync();

                return addRegion;
            }

            return null;
        }

        public async Task<Region?> DeleteRegion(string code)
        {
            var isRegionExist = await dbContext.Regions.FirstOrDefaultAsync(x => x.Code == code);

            if (isRegionExist != null)
            {
                isRegionExist.Code = code;

                dbContext.Remove(isRegionExist);
                await dbContext.SaveChangesAsync();

                return isRegionExist;
            }

            return null;
        }

        public async Task<List<Region>> GetAllRegions()
        {
            return await dbContext.Regions.ToListAsync();

        }

        public async Task<Region?> IsRegionExist(string code)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Code == code);
            
        }

        public async Task<Region> ModifyRegion(string code, Region region)
        {
            var isRegionExist = await dbContext.Regions.FirstOrDefaultAsync(x => x.Code == code);

            if (isRegionExist != null)
            {
                isRegionExist.Code = region.Code;
                isRegionExist.Name = region.Name;
                isRegionExist.RegionImageUrl = region.RegionImageUrl;

                dbContext.Update(isRegionExist);
                await dbContext.SaveChangesAsync();

                return isRegionExist;
            }

            return null;
        }
    }
}
