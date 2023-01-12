using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories.UserRepository
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> UsersList = new List<User>()
        {
            //new User()
            //{
            //    Firstname = "Read Only", Lastname = "User", Email = "readonlyuser.com",
            //    Id = Guid.NewGuid(), Username = "readonlyuser.com", Password = "readonly",
            //    UserRoles = new List<string>{"reader"}
            //},
            //new User()
            //{
            //    Firstname = "Read Write", Lastname = "User", Email = "readwriteuser.com",
            //    Id = Guid.NewGuid(), Username = "readwriteuser.com", Password = "readwrite",
            //    UserRoles = new List<string>(){"reader", "writer"}
            //}
        };
        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = UsersList.Find(x => (x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase)) && (x.Password == password));
            if (user == null)
            {
                return null;
            }

            return user;
        }
    }
}
