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
    }
}
