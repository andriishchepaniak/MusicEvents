using AutoMapper;
using BLL.DTO;
using Core.DTO;
using Models.Entities;
using SongkickEntities;

namespace BLL.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<ArtistSubscription, ArtistSubscriptionDTO>().ReverseMap();
            CreateMap<CitySubscription, CitySubscriptionDTO>().ReverseMap();
            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<Artist, ArtistDTO>().ReverseMap();
            CreateMap<Venue, VenueDTO>().ReverseMap();
            CreateMap<Performance, PerformanceDTO>().ReverseMap();
            CreateMap<State, StartDTO>().ReverseMap();
            CreateMap<LocationEvent, LocationDTO>().ReverseMap();
            CreateMap<MetroArea, MetroAreaDTO>().ReverseMap();
            CreateMap<Start, StartDTO>().ReverseMap();
        }
    }
}
