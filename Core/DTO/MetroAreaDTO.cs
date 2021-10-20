namespace Core.DTO
{
    public class MetroAreaDTO
    {
        public string uri { get; set; }
        public string displayName { get; set; }
        public CountryDTO country { get; set; }
        public int id { get; set; }
        public StateDTO state { get; set; }
    }

    public class StateDTO
    {
        public string displayName { get; set; }
    }
}