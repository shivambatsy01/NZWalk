namespace NZWalk.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }



        //navigation Property : what roles user have
        public List<User_Roles> UserRoles { get; set; }  //User_Roles can be joined on User table

    }
}
