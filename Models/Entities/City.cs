using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class City : IBaseEntity
    {
        public int Id { get; set; }
        public int CityApiId { get; set; }
        public List<User> Users { get; set; }
    }
}
