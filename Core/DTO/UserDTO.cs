using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }


        public List<ArtistAndCitySubscriptionDTO> ArtistSubscriptions { get; set; }
        public List<CitySubscriptionDTO> CitySubscriptions { get; set; }
        public List<ArtistAndCitySubscriptionDTO> ArtistAndCitySubscriptions { get; set; }
    }
}
