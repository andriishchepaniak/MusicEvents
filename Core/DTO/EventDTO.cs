using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class EventDTO
    {
        public int id { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
        public string displayName { get; set; }
        public StartDTO start { get; set; }
        public List<PerformanceDTO> performance { get; set; }
        public LocationDTO location { get; set; }
        public VenueDTO venue { get; set; }
        public string status { get; set; }
        public double popularity { get; set; }
    }
}
