namespace NZWalk.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> UserRoles { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

    }
}
