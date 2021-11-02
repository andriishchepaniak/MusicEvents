using Models.Entities;
using SongkickEntities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAll();
        Task<IEnumerable<Event>> GetAll(Expression<Func<Event, bool>> predicate);
        Task<IEnumerable<EventApi>> GetRange(int offset, int count);
        Task<IEnumerable<EventApi>> GetArtistEventsByUserId(int userId, int page);
        Task<IEnumerable<EventApi>> GetCityEventsByUserId(int userId, int page);
        Task<IEnumerable<EventApi>> GetArtistAndCityEventsByUserId(int userId, int page);
        Task<IEnumerable<Event>> GetEventsByArtistId(int artistId, int page);
        Task<EventApi> GetById(int id);
        Task<Event> Add(Event entity);
        Task<IEnumerable<Event>> AddRange(IEnumerable<Event> entities);
        Task<Event> Update(Event entity);
        Task<int> Delete(int id);
        Task<int> DeleteAll();
    }
}
