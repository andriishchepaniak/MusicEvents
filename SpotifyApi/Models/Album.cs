using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Models
{

    public class Album
    {
        public string album_type { get; set; }
        public List<Artist> artists { get; set; }
        public string id { get; set; }
        public string label { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        //public string release_date { get; set; }
        public DateTime release_date { get; set; }
        public string release_date_precision { get; set; }
        public int total_tracks { get; set; }
        //public Tracks tracks { get; set; }
        public List<Image> images { get; set; }
        public string uri { get; set; }
    }
    public class Image
    {
        public int height { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }

}
