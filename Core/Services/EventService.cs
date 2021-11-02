using AutoMapper;
using Core.Interfaces;
using Core.Mappings;
using DAL.UnitOfWorkService;
using Models.Entities;
using SongkickAPI.Interfaces;
using SongkickAPI.Services;
using SongkickEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventServiceApi _eventServiceApi;
        private readonly IMapper _mapper;
        public EventService(IUnitOfWork unitOfWork, IMapper mapper, IEventServiceApi eventServiceApi)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _eventServiceApi = eventServiceApi;
        }

        public async Task<Event> Add(Event entity)
        {
            await _unitOfWork.EventRepository.Add(entity);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        public async Task<IEnumerable<Event>> AddRange(IEnumerable<Event> entities)
        {
            await _unitOfWork.EventRepository.AddRange(entities);
            await _unitOfWork.SaveAsync();
            return entities;
        }

        public async Task<int> Delete(int id)
        {
            _unitOfWork.EventRepository.Delete(id);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAll()
        {
            _unitOfWork.EventRepository.DeleteAll();
            return await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            return await _unitOfWork.EventRepository.GetAll();
        }

        public async Task<IEnumerable<Event>> GetAll(Expression<Func<Event, bool>> predicate)
        {
            return await _unitOfWork.EventRepository.GetAll(predicate);
        }

        public async Task<IEnumerable<EventApi>> GetArtistAndCityEventsByUserId(int userId, int page)
        {
            var artistAndCituSubs = await _unitOfWork.ArtistAndCitySubscription
                .GetAll(s => s.UserId == userId);
            var result = new List<EventApi>();
            foreach (var sub in artistAndCituSubs)
            {
                var events = (await _eventServiceApi.GetArtistsUpcomingEvents(sub.ArtistId, page))
                    .Where(e => e.venue.metroArea.id == sub.CityId);
                result.AddRange(events);
            }
            return result;
        }

        public async Task<IEnumerable<EventApi>> GetArtistEventsByUserId(int userId, int page)
        {
            var artistSubs = await _unitOfWork.ArtistSubscription.GetAll(s => s.UserId == userId);
            var result = new List<EventApi>();
            foreach (var sub in artistSubs)
            {
                var events = await _eventServiceApi.GetArtistsUpcomingEvents(sub.ArtistId, page);
                result.AddRange(events);
            }
            return result;
        }

        public async Task<EventApi> GetById(int id)
        {
            return await _eventServiceApi.EventDetails(id);
        }

        public async Task<IEnumerable<EventApi>> GetCityEventsByUserId(int userId, int page)
        {
            var citySubs = await _unitOfWork.CitySubscription.GetAll(s => s.UserId == userId);
            var result = new List<EventApi>();
            foreach (var sub in citySubs)
            {
                var events = await _eventServiceApi.GetMetroUpcomingEvents(sub.CityId, page);
                result.AddRange(events);
            }
            return result;
        }

        public async Task<IEnumerable<Event>> GetEventsByArtistId(int artistId, int page)
        {
            var eventsApi = await _eventServiceApi.GetArtistsUpcomingEvents(artistId, page);
            var events = new List<Event>();
            foreach (var eventApi in eventsApi)
            {
                events.Add(EventMapping.MapToEvent(eventApi));
            }
            return events;
        }

        public Task<IEnumerable<EventApi>> GetRange(int offset, int count)
        {
            throw new NotImplementedException();
        }

        public async Task<Event> Update(Event entity)
        {
            _unitOfWork.EventRepository.Update(entity);
            await _unitOfWork.SaveAsync();
            return entity;
        }
    }
}
