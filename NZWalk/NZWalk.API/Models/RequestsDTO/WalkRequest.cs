namespace NZWalk.API.Models.RequestsDTO
{
    public class WalkRequest
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyID { get; set; }

        //No need of Navigation Properties
        //we only have these four columns in Walk Table in database
        //while returning, we are adding the region and difficulty associated with these Id, That's it
    }
}
