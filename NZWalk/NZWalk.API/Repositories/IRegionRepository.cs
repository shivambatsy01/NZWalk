using NZWalk.API.Models.Domain;
using NZWalk.API.Models.RequestsDTO;

namespace NZWalk.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllRegionsAsync();

        Task<Region> GetRegionAsync(Guid id);

        Task<Region> AddRegionAsync(Region region);

        Task<Region> DeleteRegionAsync(Guid id);

        Task<Region> UpdateRegionAsync(Guid id, Region region);
    }
}
