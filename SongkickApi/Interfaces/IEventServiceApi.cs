﻿using SongkickEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SongkickAPI.Interfaces
{
    public interface IEventServiceApi
    {
        Task<int> GetEventsCountByArtist(int artistId);
        Task<int> GetEventsCountByVenue(int venueId);
        Task<int> GetEventsCountByCity(int metroAreaId);
        Task<IEnumerable<EventApi>> GetArtistsUpcomingEvents(int artistId, int page);
        //Task<object> GetArtistsUpcomingEvents(int artistId);
        Task<IEnumerable<EventApi>> GetVenuesUpcomingEvents(int venueId, int page);
        Task<IEnumerable<EventApi>> GetMetroUpcomingEvents(int metroAreaId, int page);
        Task<EventApi> EventDetails(int eventId);
    }
}
