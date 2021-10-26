using System;
using System.Collections.Generic;
using System.Text;

namespace SongkickEntities
{
    public class Venue
    {
        public int? id { get; set; }
        public string displayName { get; set; }
        public string uri { get; set; }
        
        public double? lat { get; set; }
        public double? lng { get; set; }
        
        public MetroArea metroArea { get; set; }
    }
}
