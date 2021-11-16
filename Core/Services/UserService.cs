using Core.Interfaces;
using DAL.UnitOfWorkService;
using Models.Entities;
using SongkickAPI.Interfaces;
using SongkickEntities;
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
        private readonly IEventServiceApi _eventServiceApi;
        private readonly IArtistServiceApi _artistServiceApi;
        public UserService(IUnitOfWork unitOfWork, IEventServiceApi eventServiceApi, IArtistServiceApi artistServiceApi)
        {
            _unitOfWork = unitOfWork;
            _eventServiceApi = eventServiceApi;
            _artistServiceApi = artistServiceApi;
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

        public User SingleOrDefault(Expression<Func<User, bool>> predicate)
        {
            return _unitOfWork.UserRepository.SingleOrDefault(predicate);
        }

        public async Task<IEnumerable<EventApi>> GetUserEvents(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetById(userId);
            var result = new List<EventApi>();
            foreach(var e in user.Events)
            {
                var Event = await _eventServiceApi.EventDetails(e.EventApiId);
                result.Add(Event);
            }
            return result;
        }

        public async Task<IEnumerable<ArtistApi>> GetUserArtists(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetById(userId);
            var result = new List<ArtistApi>();
            foreach (var artist in user.Artists)
            {
                var Artist = await _artistServiceApi.GetArtistDetails(artist.ArtistApiId);
                result.Add(Artist);
            }
            return result;
        }
    }
}
