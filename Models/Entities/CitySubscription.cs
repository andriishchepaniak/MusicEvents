using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entities
{
    public class CitySubscription : IBaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CityId { get; set; }
    }
}
