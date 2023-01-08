using Microsoft.EntityFrameworkCore;
using NZWalk.API.Database;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories.WalkDifficultyRepository
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private NZWalksDbContext nZWalksDbContext;
        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }



        public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = new Guid();
            nZWalksDbContext.WalkDifficulty.Add(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteWalkDifficultyByIdAsync(Guid id)
        {
            var walkDifficulty = await nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if(walkDifficulty == null)
            {
                return null;
            }
            
            nZWalksDbContext.WalkDifficulty.Remove(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficultiesAsync()
        {
            var walkDifficulties = await nZWalksDbContext.WalkDifficulty.ToListAsync();
            return walkDifficulties;
        }

        public async Task<WalkDifficulty> GetWalkDifficultyByIdAsync(Guid id)
        {
            var walkDifficulty = await nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if(walkDifficulty == null)
            {
                return null;
            }
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingDifficulty = await nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if(existingDifficulty == null)
            {
                return null;
            }
            existingDifficulty.Code = walkDifficulty.Code;
            await nZWalksDbContext.SaveChangesAsync();
            return existingDifficulty;
        }
    }
}
