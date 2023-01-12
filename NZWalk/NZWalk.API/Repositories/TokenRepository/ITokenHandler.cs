using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories.TokenRepository
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
