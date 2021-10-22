using AutoMapper;
using Core.DTO;
using Core.Interfaces;
using DAL.UnitOfWorkService;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UserDTO> Add(UserDTO entity)
        {
            await _unitOfWork.UserRepository.Add(_mapper.Map<User>(entity));
            await _unitOfWork.SaveAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            _unitOfWork.UserRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            return _mapper
                .Map<IEnumerable<UserDTO>>(await _unitOfWork.UserRepository.GetAll());
        }

        public async Task<IEnumerable<UserDTO>> GetAll(Expression<Func<UserDTO, bool>> predicate)
        {
            var pred = _mapper.Map<Expression<Func<User, bool>>>(predicate);
            return _mapper
                .Map<IEnumerable<UserDTO>>(await _unitOfWork.UserRepository.GetAll(pred));
        }

        public async Task<UserDTO> GetById(int id)
        {
            return _mapper
                .Map<UserDTO>(await _unitOfWork.UserRepository.GetById(id));
        }

        public async Task<IEnumerable<UserDTO>> GetRange(int offset, int count)
        {
            return _mapper
                .Map<IEnumerable<UserDTO>>
                (await _unitOfWork.UserRepository.GetRange(offset, count));
        }

        public async Task<UserDTO> Update(UserDTO entity)
        {
            var usr = _mapper.Map<User>(entity);
            _unitOfWork.UserRepository.Update(usr);
            await _unitOfWork.SaveAsync();
            return entity;
        }
    }
}
