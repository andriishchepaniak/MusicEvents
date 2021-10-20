using AutoMapper;
using Core.DTO;
using Core.Interfaces;
using DAL.UnitOfWork;
using Models.Entities;
using SongkickAPI.Services;
using SongkickEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EventServiceApi _eventServiceApi;
        private readonly IMapper _mapper;
        public EventService(IUnitOfWork unitOfWork, IMapper mapper, EventServiceApi eventServiceApi)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _eventServiceApi = eventServiceApi;
        }

        public Task<EventDTO> Add(EventDTO entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EventDTO>> GetAll(Expression<Func<EventDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<EventDTO> GetById(int id)
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

        public Task<IEnumerable<EventDTO>> GetRange(int offset, int count)
        {
            throw new NotImplementedException();
        }

        public Task<EventDTO> Update(EventDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
