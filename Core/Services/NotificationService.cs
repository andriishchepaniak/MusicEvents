using Core.EmailService;
using Core.Interfaces;
using Core.Mappings;
using DAL.UnitOfWorkService;
using Models.Entities;
using Models.SpotifyEntities;
using SongkickAPI.Interfaces;
using SongkickAPI.Services;
using SongkickEntities;
using SpotifyApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventServiceApi _eventServiceApi;
        private readonly IArtistServiceApi _artistServiceApi;
        private readonly ISpotifyService _spotifyService;
        private readonly ISpotifyAccountService _spotifyAccountService;
        private readonly IMailService _mailService;
        public NotificationService(IUnitOfWork unitOfWork, 
            IEventServiceApi eventServiceApi, 
            IMailService mailService, 
            IArtistServiceApi artistServiceApi,
            ISpotifyAccountService spotifyAccountService,
            ISpotifyService spotifyService)
        {
            _unitOfWork = unitOfWork;
            _eventServiceApi = eventServiceApi;
            _mailService = mailService;
            _artistServiceApi = artistServiceApi;
            _spotifyAccountService = spotifyAccountService;
            _spotifyService = spotifyService;
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
                        var spotifyArtists = await _spotifyService.GetArtistsByName(songkickArtist.displayName, token);
                        var artistAlbums = await _spotifyService.GetAlbumsByArtistId(spotifyArtists[0].id, token);
                        foreach (var a in artistAlbums)
                        {
                            if(a.release_date > date)
                            {
                                allAlbums.Add(a);
                            }
                        }
                    }
                    EventsMailRequest eventsMailRequest = new EventsMailRequest()
                    {
                        ToEmail = user.Email,
                        Subject = "New albums notification",
                        UserName = user.FirstName + " " + user.LastName
                    };
                    await _mailService.SendAlbumsAsync(eventsMailRequest, allAlbums);
                }
            }
            return users;
        }

        public async Task<IEnumerable<User>> SendEvents()
        {
            var afterWeek = DateTime.Today.AddDays(18);
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
