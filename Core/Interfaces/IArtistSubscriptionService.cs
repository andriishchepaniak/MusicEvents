using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IArtistSubscriptionService
    {
        Task<IEnumerable<ArtistSubscriptionDTO>> GetAll();
        Task<IEnumerable<ArtistSubscriptionDTO>> GetAll(Expression<Func<ArtistSubscriptionDTO, bool>> predicate);
        Task<IEnumerable<ArtistSubscriptionDTO>> GetRange(int offset, int count);
        Task<ArtistSubscriptionDTO> GetById(int id);
        Task<ArtistSubscriptionDTO> Add(ArtistSubscriptionDTO entity);
        Task<ArtistSubscriptionDTO> Update(ArtistSubscriptionDTO entity);
        Task Delete(int id);
    }
}
