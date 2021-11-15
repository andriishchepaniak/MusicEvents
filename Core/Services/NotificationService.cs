using Core.EmailService;
using Core.Interfaces;
using DAL.UnitOfWorkService;
using Models.Entities;
using SongkickAPI.Interfaces;
using SongkickAPI.Services;
using SongkickEntities;
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
        private readonly IMailService _mailService;
        public NotificationService(IUnitOfWork unitOfWork, IEventServiceApi eventServiceApi, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _eventServiceApi = eventServiceApi;
            _mailService = mailService;
        }
        public async Task<IEnumerable<Event>> NotifyUsersAboutEvents()
        {
            var afterWeek = DateTime.Today.AddDays(24);
            var events = await _unitOfWork.EventRepository.GetAllWithUsers(e => e.Date == afterWeek);
            foreach (var item in events)
            {
                var e = await _eventServiceApi.EventDetails(item.EventApiId);
                foreach (var user in item.Users)
                {
                    EventsMailRequest eventsMailRequest = new EventsMailRequest()
                    {
                        ToEmail = user.Email,
                        Subject = "Email notification",
                        EventName = e.displayName,
                        ArtistName = e.performance[0].artist.displayName,
                        Date = e.start.date,
                        UserName = user.FirstName + " " + user.LastName,
                        City = e.location.city,
                        Venue = e.venue.displayName,
                        WebSite = e.uri
                    };
                    await _mailService.SendNotificationEmailAsync(eventsMailRequest);
                }
            }
            return events;
        }
    }
}
