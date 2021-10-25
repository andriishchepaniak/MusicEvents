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
        Task<IEnumerable<EventApi>> GetAll();
        Task<IEnumerable<EventApi>> GetAll(Expression<Func<EventApi, bool>> predicate);
        Task<IEnumerable<EventApi>> GetRange(int offset, int count);
        Task<IEnumerable<EventApi>> GetArtistEventsByUserId(int userId);
        Task<IEnumerable<EventApi>> GetCityEventsByUserId(int userId);
        Task<IEnumerable<EventApi>> GetArtistAndCityEventsByUserId(int userId);
        Task<EventApi> GetById(int id);
        Task<Event> Add(Event entity);
        Task<EventApi> Update(EventApi entity);
        Task<int> Delete(int id);
    }
}
