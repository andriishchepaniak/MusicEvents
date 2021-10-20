using AutoMapper;
using Core.Interfaces;
using DAL.UnitOfWork;
using SongkickAPI.Services;
using SongkickEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ArtistServiceApi _artistServiceApi;
        private readonly IMapper _mapper;
        public ArtistService(IUnitOfWork unitOfWork, IMapper mapper, ArtistServiceApi artistServiceApi)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _artistServiceApi = artistServiceApi;
        }
        public async Task<IEnumerable<Artist>> GetUsersArtists(int userId)
        {
            var userSubs = await _unitOfWork.ArtistSubscription.GetAll(s => s.UserId == userId);
            var result = new List<Artist>(); 
            foreach(var sub in userSubs)
            {
                var artist = await _artistServiceApi.GetArtistDetails(sub.ArtistId);
                result.Add(artist);
            }
            return result;
        }
    }
}
