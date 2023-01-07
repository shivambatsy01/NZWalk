using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllRegionsAsync(); //use task for async function
    }
}
