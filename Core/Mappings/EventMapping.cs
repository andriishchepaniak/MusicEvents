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
            return new Event()
            {
                ArtistApiId = eventApi.performance[0].artist.id,
                CityApiId = eventApi.venue.metroArea.id,
                EventApiId = eventApi.id,
                Date = DateTime.Parse(eventApi.start.date),
                DisplayName = eventApi.displayName
            };
        }
        public static IEnumerable<Event> MapToEvent(IEnumerable<EventApi> events)
        {
            var newEvents = new List<Event>();
            foreach (var e in events)
            {
                newEvents.Add(MapToEvent(e));
            }
            return newEvents;
        }
    }
}
