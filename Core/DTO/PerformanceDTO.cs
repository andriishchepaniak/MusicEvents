namespace Core.DTO
{
    public class PerformanceDTO
    {
        public ArtistDTO artist { get; set; }
        public int id { get; set; }
        public string displayName { get; set; }
        public int billingIndex { get; set; }
        public string billing { get; set; }
    }
}