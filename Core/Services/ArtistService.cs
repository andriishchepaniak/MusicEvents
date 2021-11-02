using AutoMapper;
using Core.Interfaces;
using DAL.UnitOfWorkService;
using Models.Entities;
using SongkickAPI.Interfaces;
using SongkickAPI.Services;
using SongkickEntities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArtistServiceApi _artistServiceApi;
        private readonly IMapper _mapper;
        public ArtistService(IUnitOfWork unitOfWork, IMapper mapper, IArtistServiceApi artistServiceApi)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _artistServiceApi = artistServiceApi;
        }

        public async Task<Artist> Add(Artist entity)
        {
            var artist = await _unitOfWork.ArtistRepository.Add(entity);
            await _unitOfWork.SaveAsync();
            return artist;
        }

        public async Task<int> Delete(int id)
        {
            _unitOfWork.ArtistRepository.Delete(id);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            return await _unitOfWork.ArtistRepository.GetAll();
        }

        public async Task<IEnumerable<Artist>> GetAll(Expression<Func<Artist, bool>> predicate)
        {
            return await _unitOfWork.ArtistRepository.GetAll(predicate);
        }

        public async Task<Artist> GetByField(Expression<Func<Artist, bool>> predicate)
        {
            return await _unitOfWork.ArtistRepository.GetByField(predicate);
        }

        public async Task<Artist> GetById(int id)
        {
            return await _unitOfWork.ArtistRepository.GetById(id);
        }

        public async Task<Artist> Subscribe(int userId, int artistId)
        {
            var artist = await _unitOfWork.ArtistRepository.GetById(artistId);
            var user = await _unitOfWork.UserRepository.GetById(userId);
            user.Artists.Add(artist);
            //_unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();
            return await _unitOfWork.ArtistRepository.GetById(artistId);
        }

        public async Task<IEnumerable<Artist>> GetRange(int offset, int count)
        {
            return await _unitOfWork.ArtistRepository.GetRange(offset, count);
        }

        public async Task<IEnumerable<ArtistApi>> GetUsersArtists(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetById(userId);
            var result = new List<ArtistApi>();

            foreach (var artist in user.Artists)
            {
                var artistApi = await _artistServiceApi.GetArtistDetails(artist.ArtistApiId);
                result.Add(artistApi);
            }
            
            return result;
        }

        public async Task<Artist> Update(Artist entity)
        {
            var artist =_unitOfWork.ArtistRepository.Update(entity);
            await _unitOfWork.SaveAsync();
            return artist;
        }

        public async Task<int> DeleteAll()
        {
            _unitOfWork.ArtistRepository.DeleteAll();
            return await _unitOfWork.SaveAsync();
        }
    }
}
