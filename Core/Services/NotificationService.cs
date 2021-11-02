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
            var afterWeek = DateTime.Today.AddDays(7);
            var events = await _unitOfWork.EventRepository.GetAllWithUsers(e => e.Date == afterWeek);
            foreach (var item in events)
            {
                var e = await _eventServiceApi.EventDetails(item.EventApiId);
                foreach (var user in item.Users)
                {
                    MailRequest mailRequest = new MailRequest()
                    {
                        ToEmail = user.Email,
                        Subject = e.displayName,
                        Body = e.displayName
                    };
                    await _mailService.SendEmailAsync(mailRequest);
                }
            }
            return events;
        }
    }
}
