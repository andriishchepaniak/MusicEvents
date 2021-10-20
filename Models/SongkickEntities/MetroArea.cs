using System;
using System.Collections.Generic;
using System.Text;

namespace SongkickEntities
{
    public class MetroArea
    {
        public string uri { get; set; }
        public string displayName { get; set; }
        public Country country { get; set; }
        public int id { get; set; }
        public State state { get; set; }
    }
    public class State
    { 
        public string displayName { get; set; }
    }
}
