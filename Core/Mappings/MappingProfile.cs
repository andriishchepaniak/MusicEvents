using AutoMapper;
using Core.DTO;
using Models.Entities;

namespace Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<CitySubscription, CitySubscriptionDTO>().ReverseMap();
            CreateMap<ArtistSubscription, ArtistAndCitySubscriptionDTO>().ReverseMap();
            CreateMap<ArtistAndCitySubscription, ArtistAndCitySubscriptionDTO>().ReverseMap();
        }
    }
}
