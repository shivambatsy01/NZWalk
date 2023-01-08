using Microsoft.EntityFrameworkCore;
using NZWalk.API.Database;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories.WalkRepository
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;
        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }




        public async Task<Walk> AddWalkAsync(Walk walk)
        {
            walk.Id = new Guid();
            await nZWalksDbContext.Walks.AddAsync(walk);
            await nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            var walk = await nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            {
                return null;
            }
            nZWalksDbContext.Walks.Remove(walk);
            await nZWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllWalkAsync()
        {
            return await nZWalksDbContext.Walks
                            .Include(x => x.Region)
                            .Include(x => x.WalkDifficulty)
                            .ToListAsync();
        }

        public async Task<Walk> GetWalkByIdAsync(Guid id)
        {
            return await nZWalksDbContext.Walks
                            .Include(x => x.Region)
                            .Include(x=> x.WalkDifficulty)
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            var existingWalk = await nZWalksDbContext.Walks
                                        .Include(x => x.Region)
                                        .Include(x => x.WalkDifficulty)
                                        .FirstOrDefaultAsync(x => x.Id == id);
            //however we don't need complete object in update as we are again returning resource locator

            if (existingWalk == null)
            {
                return existingWalk;
            }

            existingWalk.Name = walk.Name;
            existingWalk.WalkDifficultyID = walk.WalkDifficultyID;
            existingWalk.Length = walk.Length;
            existingWalk.RegionId = walk.RegionId;

            await nZWalksDbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
