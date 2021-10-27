using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models.Entities
{
    public class User : IBaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }

        public virtual List<Artist> Artists { get; set; }
        public virtual List<City> Cities { get; set; }
        public virtual List<ArtistAndCitySubscription> ArtistAndCitySubscriptions { get; set; }
        public virtual List<Event> Events { get; set; }
    }
}
