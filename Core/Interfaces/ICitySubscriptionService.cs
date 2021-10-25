using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICitySubscriptionService
    {
        Task<IEnumerable<CitySubscriptionDTO>> GetAll();
        Task<IEnumerable<CitySubscriptionDTO>> GetAll(Expression<Func<CitySubscriptionDTO, bool>> predicate);
        Task<IEnumerable<CitySubscriptionDTO>> GetRange(int offset, int count);
        Task<CitySubscriptionDTO> GetById(int id);
        Task<CitySubscriptionDTO> Add(CitySubscriptionDTO entity);
        Task<CitySubscriptionDTO> Update(CitySubscriptionDTO entity);
        Task<int> Delete(int id);
    }
}
