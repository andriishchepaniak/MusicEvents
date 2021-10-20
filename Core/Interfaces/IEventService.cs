using Core.DTO;
using SongkickEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDTO>> GetAll();
        Task<IEnumerable<EventDTO>> GetAll(Expression<Func<EventDTO, bool>> predicate);
        Task<IEnumerable<EventDTO>> GetRange(int offset, int count);
        Task<IEnumerable<EventApi>> GetArtistEventsByUserId(int userId);
        Task<IEnumerable<EventApi>> GetCityEventsByUserId(int userId);
        Task<EventDTO> GetById(int id);
        Task<EventDTO> Add(EventDTO entity);
        Task<EventDTO> Update(EventDTO entity);
        void Delete(int id);
    }
}
