using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAll();
        Task<IEnumerable<UserDTO>> GetAll(Expression<Func<UserDTO, bool>> predicate);
        Task<IEnumerable<UserDTO>> GetRange(int offset, int count);
        Task<UserDTO> GetById(int id);
        Task<UserDTO> Add(UserDTO entity);
        Task<UserDTO> Update(UserDTO entity);
        Task<int> Delete(int id);
    }
}
