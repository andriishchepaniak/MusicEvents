using Core.Interfaces;
using DAL.UnitOfWorkService;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<User> Add(User entity)
        {
            entity.Password = Hash(entity.Password);
            await _unitOfWork.UserRepository.Add(entity);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        public async Task<int> Delete(int id)
        {
            _unitOfWork.UserRepository.Delete(id);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _unitOfWork.UserRepository.GetAll();
        }

        public async Task<IEnumerable<User>> GetAll(Expression<Func<User, bool>> predicate)
        {
            return await _unitOfWork.UserRepository.GetAll(predicate);
        }

        public async Task<User> GetByField(Expression<Func<User, bool>> predicate)
        {
            return await _unitOfWork.UserRepository.GetByField(predicate);
        }

        public async Task<User> GetById(int id)
        {
            return await _unitOfWork.UserRepository.GetById(id);
        }

        public async Task<IEnumerable<User>> GetRange(int offset, int count)
        {
            return await _unitOfWork.UserRepository.GetRange(offset, count);
        }

        public async Task<User> Update(User entity)
        {
            _unitOfWork.UserRepository.Update(entity);
            await _unitOfWork.SaveAsync();
            return entity;
        }
        private string Hash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        } 
        public bool VerifyPasswordHash(string inputPassword, string dbPassword)
        {
            return Hash(inputPassword) == dbPassword;
        }
    }
}
