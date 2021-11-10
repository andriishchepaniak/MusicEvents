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
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IEventServiceApi _eventServiceApi;
        public SubscriptionService(
            IUnitOfWork unitOfWork,
            IEventServiceApi eventServiceApi)
        {
            _unitOfWork = unitOfWork;
            _eventServiceApi = eventServiceApi;
        }
        public enum EntityFilter
        {
            Artist,
            City
        }
        public async Task<int> AddEventsFromApi(int entityId, int totalCount, EntityFilter filter)
        {
            if (totalCount <= 50)
            {
                var events = filter == EntityFilter.Artist
                    ? EventMapping.MapToEvent(await _eventServiceApi.GetArtistsUpcomingEvents(entityId))
                    : EventMapping.MapToEvent(await _eventServiceApi.GetMetroUpcomingEvents(entityId));
                foreach (var ev in events)
                {
                    var cond = await _unitOfWork.EventRepository.Any(e => e.EventApiId == ev.EventApiId);
                    if (!cond)
                    {
                        await _unitOfWork.EventRepository.Add(ev);
                    }
                }
                return await _unitOfWork.SaveAsync();
            }
            var pages = totalCount % 50 == 0
                    ? totalCount / 50
                    : (totalCount / 50) + 1;
            for (int i = 1; i <= pages; i++)
            {
                var events = filter == EntityFilter.Artist
                ? EventMapping.MapToEvent(await _eventServiceApi.GetArtistsUpcomingEvents(entityId, i))
                : EventMapping.MapToEvent(await _eventServiceApi.GetMetroUpcomingEvents(entityId, i));
                var concert = new Event();
                var c = events.Contains(concert);
                foreach (var ev in events)
                {
                    var cond = await _unitOfWork.EventRepository.Any(e => e.EventApiId == ev.EventApiId);
                    if (!cond)
                    {
                        await _unitOfWork.EventRepository.Add(ev);
                    }
                }
            }
            return await _unitOfWork.SaveAsync();
        }
    }
}
