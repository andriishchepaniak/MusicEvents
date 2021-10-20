namespace Core.DTO
{
    public class VenueDTO
    {
        public int id { get; set; }
        public string displayName { get; set; }
        public string uri { get; set; }
        
        public double lat { get; set; }
        public double lng { get; set; }
        
        public MetroAreaDTO metroArea { get; set; }
    }
}