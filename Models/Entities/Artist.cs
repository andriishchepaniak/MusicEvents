using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Artist : IBaseEntity
    {
        public int Id { get; set; }
        public int ArtistApiId { get; set; }
        [JsonIgnore]
        public List<User> Users { get; set; }
    }
}
