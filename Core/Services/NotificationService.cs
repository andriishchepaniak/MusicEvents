using Core.EmailService;
using Core.Interfaces;
using DAL.UnitOfWorkService;
using Models.Entities;
using Models.SpotifyEntities;
using SongkickAPI.Interfaces;
using SongkickEntities;
using SpotifyApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventServiceApi _eventServiceApi;
        private readonly IArtistServiceApi _artistServiceApi;
        private readonly ISpotifyAlbumService _spotifyAlbumService;
        private readonly ISpotifyArtistService _spotifyArtistService;
        private readonly ISpotifyAccountService _spotifyAccountService;
        private readonly IMailService _mailService;
        public NotificationService(IUnitOfWork unitOfWork, 
            IEventServiceApi eventServiceApi, 
            IMailService mailService, 
            IArtistServiceApi artistServiceApi,
            ISpotifyAccountService spotifyAccountService,
            ISpotifyAlbumService spotifyAlbumService,
            ISpotifyArtistService spotifyArtistService)
        {
            _unitOfWork = unitOfWork;
            _eventServiceApi = eventServiceApi;
            _mailService = mailService;
            _artistServiceApi = artistServiceApi;
            _spotifyAccountService = spotifyAccountService;
            _spotifyAlbumService = spotifyAlbumService;
            _spotifyArtistService = spotifyArtistService;
        }

        public async Task<IEnumerable<User>> SendAlbums()
        {
            var date = DateTime.Now.AddMonths(-3);
            var users = await _unitOfWork.UserRepository.GetAll();
            foreach (var user in users)
            {
                if(user.Artists.ToList().Count != 0)
                {
                    var allAlbums = new List<Album>();
                    foreach (var artist in user.Artists.ToList())
                    {
                        var songkickArtist = await _artistServiceApi.GetArtistDetails(artist.ArtistApiId);
                        var token = await _spotifyAccountService.GetAccessToken();
                        var spotifyArtists = (await _spotifyArtistService.GetArtistsByName(songkickArtist.displayName)).ToList();
                        var artistAlbums = await _spotifyAlbumService.GetAlbumsByArtistId(spotifyArtists[0].id);
                        foreach (var a in artistAlbums)
                        {
                            if(a.release_date > date)
                            {
                                allAlbums.Add(a);
                            }
                        }
                    }
                    if(allAlbums.Count != 0)
                    {
                        EventsMailRequest eventsMailRequest = new EventsMailRequest()
                        {
                            ToEmail = user.Email,
                            Subject = "New albums notification",
                            UserName = user.FirstName + " " + user.LastName
                        };
                        await _mailService.SendAlbumsAsync(eventsMailRequest, allAlbums);
                    }
                }
            }
            return users;
        }

        public async Task<IEnumerable<User>> SendEvents()
        {
            var afterWeek = DateTime.Today.AddDays(10);
            var users = await _unitOfWork.UserRepository.GetAll();
            foreach (var user in users)
            {
                var events = user.Events.Where(e => e.Date == afterWeek).ToList();
                if(events.Count != 0)
                {
                    var eventsApi = new List<EventApi>();
                    foreach (var e in events)
                    {
                        var eventApi = await _eventServiceApi.EventDetails(e.EventApiId);
                        eventsApi.Add(eventApi);
                    }
                    EventsMailRequest eventsMailRequest = new EventsMailRequest()
                    {
                        ToEmail = user.Email,
                        Subject = "Music events notification",
                        UserName = user.FirstName + " " + user.LastName
                    };
                    await _mailService.SendEventsAsync(eventsMailRequest, eventsApi);
                }
            }
            return users;
        }
    }
}
