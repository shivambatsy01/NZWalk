namespace NZWalk.API.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }  //Guid: ised for hexadecimal digit sequence format
        public string Name { get; set; }    
        public string Code { get; set; }
        public double Area { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public long Population { get; set; }

        public IEnumerable<Walk> Walks { get; set; }

    }
}
