using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.SpotifyEntities
{
    public class TracksResult
    {
        public string href { get; set; }
        public List<TrackSpotify> items { get; set; }
    }
}
