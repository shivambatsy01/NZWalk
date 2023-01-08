namespace NZWalk.API.Models.RequestsDTO
{
    public class WalkRequest
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyID { get; set; }
    }
}
