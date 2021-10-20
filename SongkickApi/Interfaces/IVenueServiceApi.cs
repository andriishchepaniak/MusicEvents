using SongkickEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SongkickAPI.Interfaces
{
    public interface IVenueServiceApi
    {
        Task<IEnumerable<Venue>> GetVenuesByName(string venueName);
        Task<Venue> GetVenueDetails(int venueId);
    }
}
