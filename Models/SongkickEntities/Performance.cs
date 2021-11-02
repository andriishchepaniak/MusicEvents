using System;
using System.Collections.Generic;
using System.Text;

namespace SongkickEntities
{
    public class Performance
    {
        public ArtistApi artist { get; set; }
        public int id { get; set; }
        public string displayName { get; set; }
        public int billingIndex { get; set; }
        public string billing { get; set; }
    }
}
