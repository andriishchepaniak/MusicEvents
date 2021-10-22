using System;
using System.Collections.Generic;
using System.Text;

namespace SongkickEntities
{
    public class City
    {
        public int id { get; set; }
        public string uri { get; set; }
        public string displayName { get; set; }
        public Country country { get; set; }
        public double? lng { get; set; }
        public double? lat { get; set; }
    }
}
