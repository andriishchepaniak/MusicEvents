using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entities
{
    public class ArtistSubscription : IBaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ArtistId { get; set; }
    }
}
