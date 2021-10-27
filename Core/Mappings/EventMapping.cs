using Models.Entities;
using SongkickEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mappings
{
    public class EventMapping
    {
        public static Event MapToEvent(EventApi eventApi)
        {
            return new Event
            {
                Id = eventApi.id,
                ArtistApiId = eventApi.performance[0].artist.id,
                Date = DateTime.Parse(eventApi.start.date),
                DisplayName = eventApi.displayName
            };
        }
    }
}
