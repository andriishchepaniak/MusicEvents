﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class ArtistAndCitySubscription : IBaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ArtistId { get; set; }
        public int CituId { get; set; }
    }
}
