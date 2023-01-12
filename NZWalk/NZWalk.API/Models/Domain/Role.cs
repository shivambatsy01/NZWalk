namespace NZWalk.API.Models.Domain
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }


        //navigation property : sahring one key together
        public List<User_Roles> UserRoles { get; set; }
    }
}
