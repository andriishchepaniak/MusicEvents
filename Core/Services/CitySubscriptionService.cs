using AutoMapper;
using Core.Interfaces;
using DAL.UnitOfWorkService;
using Models.Entities;
using SongkickAPI.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CitySubscriptionService : SubscriptionService, ICitySubscriptionService
    {
        private readonly IMapper _mapper;
        private readonly IArtistServiceApi _artistServiceApi;
        public CitySubscriptionService(
            IUnitOfWork unitOfWork,
            IArtistServiceApi artistServiceApi,
            IEventServiceApi eventServiceApi,
            IMapper mapper) : base(unitOfWork, eventServiceApi)
        {
            _mapper = mapper;
            _artistServiceApi = artistServiceApi;
        }
        private async Task<City> CreateCitySubscribe(int cityApiId, int userId)
        {
            var user = await _unitOfWork.UserRepository.GetById(userId);
            var city = await _unitOfWork.CityRepository.GetByField(c => c.CityApiId == cityApiId);
            if (!user.Cities.Any(c => c.CityApiId == city.CityApiId))
            {
                user.Cities.Add(city);
            }
            var events = await _unitOfWork.EventRepository.GetAll(e => e.CityApiId == cityApiId);
            foreach (var ev in events)
            {
                if (!user.Events.Any(e => e.EventApiId == ev.EventApiId))
                {
                    user.Events.Add(ev);
                }
            }
            await _unitOfWork.SaveAsync();
            return await _unitOfWork.CityRepository.GetByField(a => a.CityApiId == cityApiId);
        }
        public async Task<City> SubscribeToCity(int cityApiId, int userId)
        {
            var city = await _unitOfWork.CityRepository.GetByField(a => a.CityApiId == cityApiId);
            if (city == null)
            {
                var cityFromApi = new City() { CityApiId = cityApiId };
                var totalCount = await _eventServiceApi.GetEventsCountByCity(cityApiId);
                await _unitOfWork.CityRepository.Add(cityFromApi);
                await AddEventsFromApi(cityApiId, totalCount, EntityFilter.City);
                return await CreateCitySubscribe(cityApiId, userId);
            }
            return await CreateCitySubscribe(cityApiId, userId);
        }
        public async Task<City> UnSubscribeFromCity(int cityApiId, int userId)
        {
            var user = await _unitOfWork.UserRepository.GetById(userId);
            user.Cities.RemoveAll(c => c.CityApiId == cityApiId);
            foreach (var e in user.Events.ToList())
            {
                if (e.CityApiId == cityApiId)
                {
                    if (!user.Artists.Any(a => a.ArtistApiId == e.ArtistApiId)
                        && !user.ArtistAndCitySubscriptions.Any(s => s.CityId == e.CityApiId && s.ArtistId == e.ArtistApiId))
                    {
                        user.Events.Remove(e);
                    }
                }
            }
            await _unitOfWork.SaveAsync();
            return await _unitOfWork.CityRepository.GetByField(c => c.CityApiId == cityApiId);
        }
    }
}
