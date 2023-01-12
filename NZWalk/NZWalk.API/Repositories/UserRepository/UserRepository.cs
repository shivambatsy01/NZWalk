using Microsoft.EntityFrameworkCore;
using NZWalk.API.Database;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private NZWalksDbContext nZWalksDbContext;
        public UserRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }


        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = await nZWalksDbContext.Users.FirstOrDefaultAsync(x => (x.Username.ToLower() == username.ToLower()) && (x.Password == password));
            if (user == null)
            {
                return null;
            }

            var userRoles = await nZWalksDbContext.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();
            user.UserRoles = userRoles;
            var roles = nZWalksDbContext.Roles.ToListAsync();

            user.UserRoles.ForEach(x => x.Role = roles.Result.FirstOrDefault(y => y.Id == x.RoleId));
            return user;

        }
    }
}
