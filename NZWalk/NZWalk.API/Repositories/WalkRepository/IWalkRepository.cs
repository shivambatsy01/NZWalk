using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories.WalkRepository
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllWalkAsync();

        Task<Walk> GetWalkByIdAsync(Guid id);

        Task<Walk> AddWalkAsync(Walk walk);

        Task<Walk> UpdateWalkAsync(Guid id, Walk walk);

        Task<Walk> DeleteWalkAsync(Guid id);


    }
}
