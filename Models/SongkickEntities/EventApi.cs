using System;
using System.Collections.Generic;
using System.Text;

namespace SongkickEntities
{
    public class EventApi
    {
        public int id { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
        public string displayName { get; set; }
        public Start start { get; set; }
        public List<Performance> performance { get; set; }
        public LocationEvent location { get; set; }
        public Venue venue { get; set; }
        public string status { get; set; }
        public double popularity { get; set; }
    }
}
