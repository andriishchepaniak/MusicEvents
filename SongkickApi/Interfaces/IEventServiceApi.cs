using SongkickEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SongkickAPI.Interfaces
{
    public interface IEventServiceApi
    {
        Task<IEnumerable<EventApi>> GetArtistsUpcomingEvents(int artistId);
        Task<IEnumerable<EventApi>> GetVenuesUpcomingEvents(int venueId);
        Task<IEnumerable<EventApi>> GetMetroUpcomingEvents(int venueId);
        Task<EventApi> EventDetails(int eventId);
    }
}
