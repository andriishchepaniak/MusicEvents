using AutoMapper;
using Core.Interfaces;
using DAL.UnitOfWorkService;
using Models.Entities;
using SongkickAPI.Interfaces;
using SongkickAPI.Services;
using SongkickEntities;
using System;
using System.Collections.Generic;
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

        public Task<Event> Add(Event entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventApi>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventApi>> GetAll(Expression<Func<EventApi, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EventApi>> GetArtistEventsByUserId(int userId)
        {
            var artistSubs = await _unitOfWork.ArtistSubscription.GetAll(s => s.UserId == userId);
            var result = new List<EventApi>();
            foreach (var sub in artistSubs)
            {
                var events = await _eventServiceApi.GetArtistsUpcomingEvents(sub.ArtistId);
                result.AddRange(events);
            }
            return result;
        }

        public Task<EventApi> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EventApi>> GetCityEventsByUserId(int userId)
        {
            var citySubs = await _unitOfWork.CitySubscription.GetAll(s => s.UserId == userId);
            var result = new List<EventApi>();
            foreach (var sub in citySubs)
            {
                var events = await _eventServiceApi.GetMetroUpcomingEvents(sub.CityId);
                result.AddRange(events);
            }
            return result;
        }

        public Task<IEnumerable<EventApi>> GetRange(int offset, int count)
        {
            throw new NotImplementedException();
        }

        public Task<EventApi> Update(EventApi entity)
        {
            throw new NotImplementedException();
        }
    }
}
