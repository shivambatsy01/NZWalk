using Microsoft.IdentityModel.Tokens;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User>AuthenticateUserAsync(string username, string password);
    }
}
