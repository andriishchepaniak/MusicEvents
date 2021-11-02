using Models.Entities;
using SongkickEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IArtistService
    {
        Task<IEnumerable<ArtistApi>> GetUsersArtists(int userId);
        Task<IEnumerable<Artist>> GetAll();
        Task<IEnumerable<Artist>> GetAll(Expression<Func<Artist, bool>> predicate);
        Task<Artist> GetByField(Expression<Func<Artist, bool>> predicate);
        Task<IEnumerable<Artist>> GetRange(int offset, int count);
        Task<Artist> GetById(int id);
        Task<Artist> Subscribe(int artistId, int userId);
        Task<Artist> Add(Artist entity);
        Task<Artist> Update(Artist entity);
        Task<int> Delete(int id);
        Task<int> DeleteAll();
    }
}
