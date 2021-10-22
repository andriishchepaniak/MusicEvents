using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class ArtistAndCitySubscriptionDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ArtistId { get; set; }
        public int CityId { get; set; }
    }
}
