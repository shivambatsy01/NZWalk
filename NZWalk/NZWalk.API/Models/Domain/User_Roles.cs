namespace NZWalk.API.Models.Domain
{
    public class User_Roles
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public int RoleId { get; set; }


        //navigation Property
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
