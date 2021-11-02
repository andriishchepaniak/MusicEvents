using Core.Interfaces;
using Core.Mappings;
using DAL.UnitOfWorkService;
using Models.Entities;
using SongkickAPI.Interfaces;
using SongkickAPI.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArtistServiceApi _artistServiceApi;
        private readonly IEventServiceApi _eventServiceApi;
        public SubscriptionService(
            IUnitOfWork unitOfWork,
            IArtistServiceApi artistServiceApi,
            IEventServiceApi eventServiceApi)
        {
            _unitOfWork = unitOfWork;
            _artistServiceApi = artistServiceApi;
            _eventServiceApi = eventServiceApi;
        }
        #region CreateSubscribes
        private async Task<Artist> CreateArtistSubscribe(int artistApiId, int userId)
        {
            var user = await _unitOfWork.UserRepository.GetById(userId);
            var artist = await _unitOfWork.ArtistRepository.GetByField(a => a.ArtistApiId == artistApiId);
            
            if (!user.Artists.Any(a => a.ArtistApiId == artist.ArtistApiId))
            {
                user.Artists.Add(artist);
            }
            //user.Artists.Add(artist);
            var events = await _unitOfWork.EventRepository.GetAll
                (e => e.ArtistApiId == artistApiId);
            foreach (var ev in events)
            {
                if (!user.Events.Any(e => e.EventApiId == ev.EventApiId))
                {
                    user.Events.Add(ev);
                }
            }
            //user.Events.AddRange(events);
            await _unitOfWork.SaveAsync();
            return await _unitOfWork.ArtistRepository.GetByField(a => a.ArtistApiId == artistApiId);
        }
        private async Task<City> CreateCitySubscribe(int cityApiId, int userId)
        {
            var user = await _unitOfWork.UserRepository.GetById(userId);
            var city = await _unitOfWork.CityRepository.GetByField(c => c.CityApiId == cityApiId);
            if(!user.Cities.Any(c => c.CityApiId == city.CityApiId))
            {
                user.Cities.Add(city);
            }
            var events = await _unitOfWork.EventRepository.GetAll
                (e => e.CityApiId == cityApiId);
            foreach (var ev in events)
            {
                if(!user.Events.Any(e => e.EventApiId == ev.EventApiId))
                {
                    user.Events.Add(ev);
                }
            }
            //user.Events.AddRange(events);
            await _unitOfWork.SaveAsync();
            return await _unitOfWork.CityRepository.GetByField(a => a.CityApiId == cityApiId);
        }
        private async Task<ArtistAndCitySubscription> CreateArtistAndCitySubscribe(int artistApiId, int cityApiId, int userId)
        {
            var user = await _unitOfWork.UserRepository.GetById(userId);
            var city = await _unitOfWork.CityRepository.GetByField(c => c.CityApiId == cityApiId);
            var artist = await _unitOfWork.ArtistRepository.GetByField(a => a.ArtistApiId == artistApiId);

            if (artist == null)
            {
                var artistFromApi = ArtistMapping.MapToArtist(await _artistServiceApi.GetArtistDetails(artistApiId));
                await _unitOfWork.ArtistRepository.Add(artistFromApi);
                var totalCount = await _eventServiceApi.GetEventsCountByArtist(artistApiId);
                await AddArtistEventsFromApi(artistApiId, totalCount);
            }
            if (city == null)
            {
                var cityFromApi = new City() { CityApiId = cityApiId };
                await _unitOfWork.CityRepository.Add(cityFromApi);
                var totalCount = await _eventServiceApi.GetEventsCountByCity(cityApiId);
                await AddCityEventsFromApi(cityApiId, totalCount);
            }
            var subscription = new ArtistAndCitySubscription()
            {
                ArtistId = artistApiId,
                CityId = cityApiId,
                UserId = userId
            };
            var result = await _unitOfWork.ArtistAndCitySubscription.Add(subscription);
            var events = await GetArtistAndCityEvents(artistApiId, cityApiId);
            foreach (var item in events)
            {
                var cond = user.Events.Any(e => e.EventApiId == item.EventApiId);
                if (!cond)
                {
                    user.Events.Add(item);
                }
            }
            await _unitOfWork.SaveAsync();
            return subscription;
        }
        #endregion

        #region AddEventsToDB
        private async Task AddCityEventsFromApi(int cityId, int totalCount)
        {
            if (totalCount <= 50)
            {
                var events = EventMapping.MapToEvent(await _eventServiceApi.GetMetroUpcomingEvents(cityId));
                foreach (var ev in events)
                {
                    var cond = await _unitOfWork.EventRepository.Any(e => e.EventApiId == ev.EventApiId);
                    if (!cond)
                    {
                        await _unitOfWork.EventRepository.Add(ev);
                    }
                }
                //await _unitOfWork.EventRepository.AddRange(EventMapping.MapToEvent(events));
                await _unitOfWork.SaveAsync();
            }
            else
            {
                var pages = totalCount % 50 == 0
                    ? totalCount / 50
                    : (totalCount / 50) + 1;
                for (int i = 1; i <= pages; i++)
                {
                    var events = EventMapping.MapToEvent(await _eventServiceApi.GetMetroUpcomingEvents(cityId, i));
                    foreach (var ev in events)
                    {
                        var cond = await _unitOfWork.EventRepository.Any(e => e.EventApiId == ev.EventApiId);
                        if (!cond)
                        {
                            await _unitOfWork.EventRepository.Add(ev);
                        }
                    }
                    //await _unitOfWork.EventRepository.AddRange(EventMapping.MapToEvent(events));
                }
                await _unitOfWork.SaveAsync();
            }
        }
        private async Task AddArtistEventsFromApi(int artistId, int totalCount)
        {
            if (totalCount <= 50)
            {
                var events = EventMapping.MapToEvent(await _eventServiceApi.GetArtistsUpcomingEvents(artistId));
                foreach (var ev in events)
                {
                    var cond = await _unitOfWork.EventRepository.Any(e => e.EventApiId == ev.EventApiId);
                    if (!cond)
                    {
                        await _unitOfWork.EventRepository.Add(ev);
                    }
                }
                //await _unitOfWork.EventRepository.AddRange(EventMapping.MapToEvent(events));
                await _unitOfWork.SaveAsync();
            }
            else
            {
                var pages = totalCount % 50 == 0
                    ? totalCount / 50
                    : (totalCount / 50) + 1;
                for (int i = 1; i < pages; i++)
                {
                    var events =  EventMapping.MapToEvent(await _eventServiceApi.GetArtistsUpcomingEvents(artistId, i));
                    foreach (var ev in events)
                    {
                        var cond = await _unitOfWork.EventRepository.Any(e => e.EventApiId == ev.EventApiId);
                        if (!cond)
                        {
                            await _unitOfWork.EventRepository.Add(ev);
                        }
                    }
                    //await _unitOfWork.EventRepository.AddRange(EventMapping.MapToEvent(events));
                }
                await _unitOfWork.SaveAsync();
            }
        }
        #endregion

        private async Task<IEnumerable<Event>> GetArtistAndCityEvents(int artistApiId, int cityApiId)
        {
            var artistEvents = await _eventServiceApi.GetArtistsUpcomingEvents(artistApiId);
            var result = artistEvents
                .Where(e => e.venue.metroArea.id == cityApiId)
                .ToList();
            return EventMapping.MapToEvent(result);
        }
        public async Task<Artist> SubscribeToArtist(int artistApiId, int userId)
        {
            var artist = await _unitOfWork.ArtistRepository.GetByField(a => a.ArtistApiId == artistApiId);
            if(artist == null)
            {
                var artistFromApi = ArtistMapping.MapToArtist
                    (await _artistServiceApi.GetArtistDetails(artistApiId));
                var totalCount = await _eventServiceApi.GetEventsCountByArtist(artistApiId);
                await _unitOfWork.ArtistRepository.Add(artistFromApi);
                await AddArtistEventsFromApi(artistApiId, totalCount);
                return await CreateArtistSubscribe(artistApiId, userId);
            }
            else
            {
                return await CreateArtistSubscribe(artistApiId, userId);
            }
        }

        public async Task<ArtistAndCitySubscription> SubscribeToArtistAndCity(int artistApiId, int cityApiId, int userId)
        {
            return await CreateArtistAndCitySubscribe(artistApiId, cityApiId, userId);
        }

        public async Task<City> SubscribeToCity(int cityApiId, int userId)
        {
            var city = await _unitOfWork.CityRepository.GetByField(a => a.CityApiId == cityApiId);
            if (city == null)
            {
                var cityFromApi = new City() { CityApiId = cityApiId };
                var totalCount = await _eventServiceApi.GetEventsCountByCity(cityApiId);
                await _unitOfWork.CityRepository.Add(cityFromApi);
                await AddCityEventsFromApi(cityApiId, totalCount);
                return await CreateCitySubscribe(cityApiId, userId);
            }
            else
            {
                return await CreateCitySubscribe(cityApiId, userId);
            }
        }
    }
}
