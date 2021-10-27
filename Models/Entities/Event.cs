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
        public DateTime Date { get; set; }
        public int EventApiId { get; set; }

        public int ArtistApiId { get; set; }
        public int CityApiId { get; set; }

        public List<User> Users { get; set; }
    }
}
