using System;
using System.Collections.Generic;
using System.Text;

namespace SongkickEntities
{
    public class ArtistApi
    {
        public string uri { get; set; }
        public string displayName { get; set; }
        public int id { get; set; }
        public List<object> identifier { get; set; }
    }
}
