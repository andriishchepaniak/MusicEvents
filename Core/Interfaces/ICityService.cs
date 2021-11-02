using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAll();
        Task<IEnumerable<City>> GetAll(Expression<Func<City, bool>> predicate);
        Task<IEnumerable<City>> GetRange(int offset, int count);
        Task<City> GetById(int id);
        Task<City> Add(City entity);
        Task<City> Update(City entity);
        Task<int> Delete(int id);
        Task<int> DeleteAll();
    }
}
