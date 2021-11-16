using AutoMapper;
using Core.Interfaces;
using Core.Mappings;
using DAL.UnitOfWorkService;
using Models.Entities;
using SongkickAPI.Interfaces;
using SongkickEntities;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ArtistSubscriptionService : SubscriptionService, IArtistSubscriptionService
    {
        private readonly IMapper _mapper;
        private readonly IArtistServiceApi _artistServiceApi;
        public ArtistSubscriptionService(
            IUnitOfWork unitOfWork,
            IArtistServiceApi artistServiceApi,
            IEventServiceApi eventServiceApi,
            IMapper mapper) : base(unitOfWork, eventServiceApi)
        {
            _mapper = mapper;
            _artistServiceApi = artistServiceApi;
        }

        private async Task<ArtistApi> CreateArtistSubscribe(int artistApiId, int userId)
        {
            var user = await _unitOfWork.UserRepository.GetById(userId);
            var artist = await _unitOfWork.ArtistRepository.GetByField(a => a.ArtistApiId == artistApiId);

            if (!user.Artists.Any(a => a.ArtistApiId == artist.ArtistApiId))
            {
                user.Artists.Add(artist);
            }

            var events = await _unitOfWork.EventRepository.GetAll(e => e.ArtistApiId == artistApiId);

            foreach (var ev in events)
            {
                if (!user.Events.Any(e => e.EventApiId == ev.EventApiId))
                {
                    user.Events.Add(ev);
                }
            }

            await _unitOfWork.SaveAsync();
            return await _artistServiceApi.GetArtistDetails(artistApiId);
        }

        public async Task<ArtistApi> SubscribeToArtist(int artistApiId, int userId)
        {
            var artist = await _unitOfWork.ArtistRepository.GetByField(a => a.ArtistApiId == artistApiId);
            if (artist == null)
            {
                var artistFromApi = ArtistMapping.MapToArtist(await _artistServiceApi.GetArtistDetails(artistApiId));
                var totalCount = await _eventServiceApi.GetEventsCountByArtist(artistApiId);
                await _unitOfWork.ArtistRepository.Add(artistFromApi);
                await _unitOfWork.SaveAsync();
                await AddEventsFromApi(artistApiId, totalCount, EntityFilter.Artist);
                return await CreateArtistSubscribe(artistApiId, userId);
            }
            return await CreateArtistSubscribe(artistApiId, userId);
        }
        public async Task<ArtistApi> UnSubscribeFromArtist(int artistApiId, int userId)
        {
            var user = await _unitOfWork.UserRepository.GetById(userId);
            user.Artists.RemoveAll(a => a.ArtistApiId == artistApiId);
            //user.Events.RemoveAll((e => e.ArtistApiId == artistApiId) && )
            foreach (var e in user.Events.ToList())
            {
                if(e.ArtistApiId == artistApiId)
                {
                    if(!user.Cities.Any(c => c.CityApiId == e.CityApiId) 
                        && !user.ArtistAndCitySubscriptions.Any(s => s.CityId == e.CityApiId && s.ArtistId == e.ArtistApiId))
                    {
                        user.Events.Remove(e);
                    }
                }
            }
            await _unitOfWork.SaveAsync();
            return await _artistServiceApi.GetArtistDetails(artistApiId);
        }
    }
}
