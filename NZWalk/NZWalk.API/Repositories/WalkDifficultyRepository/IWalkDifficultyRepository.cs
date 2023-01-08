using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories.WalkDifficultyRepository
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficultiesAsync();

        Task<WalkDifficulty> GetWalkDifficultyByIdAsync(Guid id);

        Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, WalkDifficulty walkDifficulty);

        Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty);

        Task<WalkDifficulty> DeleteWalkDifficultyByIdAsync(Guid id);
    }
}
