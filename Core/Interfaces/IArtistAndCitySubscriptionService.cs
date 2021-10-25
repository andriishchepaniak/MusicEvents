using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IArtistAndCitySubscriptionService
    {
        Task<IEnumerable<ArtistAndCitySubscriptionDTO>> GetAll();
        Task<IEnumerable<ArtistAndCitySubscriptionDTO>> GetAll
            (Expression<Func<ArtistAndCitySubscriptionDTO, bool>> predicate);
        Task<IEnumerable<ArtistAndCitySubscriptionDTO>> GetRange(int offset, int count);
        Task<ArtistAndCitySubscriptionDTO> GetById(int id);
        Task<ArtistAndCitySubscriptionDTO> Add(ArtistAndCitySubscriptionDTO entity);
        Task<ArtistAndCitySubscriptionDTO> Update(ArtistAndCitySubscriptionDTO entity);
        Task<int> Delete(int id);
    }
}
