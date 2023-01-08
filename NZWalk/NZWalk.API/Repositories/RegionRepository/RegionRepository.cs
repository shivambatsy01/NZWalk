using Microsoft.EntityFrameworkCore;
using NZWalk.API.Database;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.RequestsDTO;

namespace NZWalk.API.Repositories.RegionRepository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;
        public RegionRepository(NZWalksDbContext nZWalksDbContext)  //who is calling this ctor and where we are passing parameter nzwalksdbcontext : Dependency injection
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<IEnumerable<Region>> GetAllRegionsAsync()
        {
            return await nZWalksDbContext.Regions.ToListAsync();

            //nzwalksDbcontext.Regions is of type Dbset<region> -> get list of all region
            //we are not running any stored procedure here,
            //EntityFramework is taking care of all it as it's a dependency
            //if we call dbcontext.walks, it will return us all data of walks and repository is returning it by converting into list
        }

        public async Task<Region> GetRegionAsync(Guid id)
        {
            return await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> AddRegionAsync(Region region)
        {
            region.Id = Guid.NewGuid(); //assigning new Id to region
            await nZWalksDbContext.AddAsync(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
            //if changes are not saved, it wont return from this method
            //async function call need to be awaited
        }

        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            var region = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }

            nZWalksDbContext.Regions.Remove(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            var existingRegion = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Name = region.Name;
            existingRegion.Walks = region.Walks;
            existingRegion.Area = region.Area;
            existingRegion.Population = region.Population;
            existingRegion.Longitude = region.Longitude;
            existingRegion.Latitude = region.Latitude;
            //no change in id

            await nZWalksDbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
