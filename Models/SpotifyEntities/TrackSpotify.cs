using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.SpotifyEntities
{
    public class TrackSpotify
    {
        public string id { get; set; }
        public List<ArtistSpotify> artists { get; set; }
        public Album album { get; set; }
        public string name { get; set; }
        public string preview_url { get; set; }
    }
}
