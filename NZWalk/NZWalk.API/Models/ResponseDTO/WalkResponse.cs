namespace NZWalk.API.Models.ResponseDTO
{
    public class WalkResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyID { get; set; }


        //Navigation Properties
        public RegionResponse Region { get; set; }
        public WalkDifficultyResponse WalkDifficulty { get; set; }
    }
}
