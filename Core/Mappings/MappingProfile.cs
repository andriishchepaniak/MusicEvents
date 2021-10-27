using AutoMapper;
using Core.DTO;
using Models.Entities;
using SongkickEntities;

namespace Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<CitySubscription, CitySubscriptionDTO>().ReverseMap();
            CreateMap<ArtistAndCitySubscription, ArtistAndCitySubscriptionDTO>().ReverseMap();
            CreateMap<ArtistSubscription, ArtistSubscriptionDTO>().ReverseMap();

            //CreateMap<Event, EventApi>()
            //    .ForMember(x => x.id, m => m.MapFrom(x => x.Id))
            //    .ForPath(x => x.start.date, m => m.MapFrom(x => x.Date))
            //    .ForMember(x => x.displayName, m => m.MapFrom(x => x.DisplayName))
            //    //.ForPath(x => x.performance[0].artist.id, m => m.MapFrom(x => x.ArtistId))
            //    .ForAllMembers(m => m.Ignore());
        }
    }
}
