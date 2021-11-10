using AutoMapper;
using Core.Interfaces;
using Core.Mappings;
using DAL.UnitOfWorkService;
using Models.Entities;
using SongkickAPI.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ArtistAndCitySubscriptionService : SubscriptionService, IArtistAndCitySubscriptionService
    {
        private readonly IMapper _mapper;
        private readonly IArtistServiceApi _artistServiceApi;
        public ArtistAndCitySubscriptionService(
            IUnitOfWork unitOfWork,
            IArtistServiceApi artistServiceApi,
            IEventServiceApi eventServiceApi,
            IMapper mapper) : base(unitOfWork, eventServiceApi)
        {
            _mapper = mapper;
            _artistServiceApi = artistServiceApi;
        }
        private async Task<ArtistAndCitySubscription> CreateArtistAndCitySubscribe(int artistApiId, int cityApiId, int userId)
        {
            var user = await _unitOfWork.UserRepository.GetById(userId);
            
            var subscription = new ArtistAndCitySubscription()
            {
                ArtistId = artistApiId,
                CityId = cityApiId,
                UserId = userId
            };
            var result = await _unitOfWork.ArtistAndCitySubscription.Add(subscription);
            var events = await _unitOfWork.EventRepository.GetAll(e => e.ArtistApiId == artistApiId && e.CityApiId == cityApiId);
            foreach (var item in events)
            {
                var cond = user.Events.Any(e => e.EventApiId == item.EventApiId);
                if (!cond)
                {
                    user.Events.Add(item);
                }
            }
            await _unitOfWork.SaveAsync();
            return subscription;
        }
        public async Task<ArtistAndCitySubscription> SubscribeToArtistAndCity(int artistApiId, int cityApiId, int userId)
        {
            var city = await _unitOfWork.CityRepository.GetByField(c => c.CityApiId == cityApiId);
            var artist = await _unitOfWork.ArtistRepository.GetByField(a => a.ArtistApiId == artistApiId);

            if (artist == null)
            {
                var artistFromApi = ArtistMapping.MapToArtist(await _artistServiceApi.GetArtistDetails(artistApiId));
                await _unitOfWork.ArtistRepository.Add(artistFromApi);
                var totalCount = await _eventServiceApi.GetEventsCountByArtist(artistApiId);
                await AddEventsFromApi(artistApiId, totalCount, EntityFilter.Artist);
            }
            if (city == null)
            {
                var cityFromApi = new City() { CityApiId = cityApiId };
                await _unitOfWork.CityRepository.Add(cityFromApi);
                var totalCount = await _eventServiceApi.GetEventsCountByCity(cityApiId);
                await AddEventsFromApi(cityApiId, totalCount, EntityFilter.City);
            }
            return await CreateArtistAndCitySubscribe(artistApiId, cityApiId, userId);
        }
    }
}
