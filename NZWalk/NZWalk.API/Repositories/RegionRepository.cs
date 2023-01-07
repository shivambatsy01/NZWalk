using Microsoft.EntityFrameworkCore;
using NZWalk.API.Database;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;
        public RegionRepository(NZWalksDbContext nZWalksDbContext)
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
    }
}
