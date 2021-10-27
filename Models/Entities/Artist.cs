using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Artist
    {
        public int Id { get; set; }
        public int ArtistApiId { get; set; }
        public List<User> Users { get; set; }
    }
}
