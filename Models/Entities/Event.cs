using SongkickEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entities
{
    public class Event : IBaseEntity
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }
        public string Status { get; set; }
        public double Popularity { get; set; }
        public DateTime Date { get; set; }
        public int ArtistId { get; set; }
        public bool FlaggedAsEnded { get; set; }
        public int VenueId { get; set; }
        public int MetroAreId { get; set; }
        public string City { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
