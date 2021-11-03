using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EmailService
{
    public class EventsMailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string UserName { get; set; }
        public string EventName { get; set; }
        public string ArtistName { get; set; }
        public string Date { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
        public string WebSite { get; set; }
    }
}
