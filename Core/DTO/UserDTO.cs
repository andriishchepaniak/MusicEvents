using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }


        public virtual List<ArtistSubscriptionDTO> ArtistSubscriptions { get; set; }
        public List<CitySubscriptionDTO> EventSubscriptions { get; set; }
    }
}
