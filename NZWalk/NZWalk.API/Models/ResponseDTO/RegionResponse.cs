using NZWalk.API.Models.Domain;

namespace NZWalk.API.Models.ResponseDTO
{
    public class RegionResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public double Area { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public long Population { get; set; }

        public IEnumerable<Walk> Walks { get; set; }
    }
}
