using NZWalk.API.Models.Domain;

namespace NZWalk.API.Models.RequestsDTO
{
    public class RegionRequest
    {
        public string Name { get; set; } //here no id as client won't pass id, server will alott new id to datarow tuple
        public string Code { get; set; }
        public double Area { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public long Population { get; set; }
    }
}
