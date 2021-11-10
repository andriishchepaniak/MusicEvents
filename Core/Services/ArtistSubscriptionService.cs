using AutoMapper;
using Core.Interfaces;
using Core.Mappings;
using DAL.UnitOfWorkService;
using SongkickAPI.Interfaces;
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

        private async Task<int> CreateArtistSubscribe(int artistApiId, int userId)
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

            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> SubscribeToArtist(int artistApiId, int userId)
        {
            var artist = await _unitOfWork.ArtistRepository.GetByField(a => a.ArtistApiId == artistApiId);
            if (artist == null)
            {
                var artistFromApi = ArtistMapping.MapToArtist(await _artistServiceApi.GetArtistDetails(artistApiId));
                var totalCount = await _eventServiceApi.GetEventsCountByArtist(artistApiId);
                await _unitOfWork.ArtistRepository.Add(artistFromApi);
                await AddEventsFromApi(artistApiId, totalCount, EntityFilter.Artist);
                return await CreateArtistSubscribe(artistApiId, userId);
            }
            return await CreateArtistSubscribe(artistApiId, userId);
        }
    }
}
