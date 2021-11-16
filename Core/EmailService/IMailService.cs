using Models.SpotifyEntities;
using SongkickEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EmailService
{
    public interface IMailService
    {
        Task SendEventsAsync(EventsMailRequest eventsmailRequest, List<EventApi> events);
        Task SendAlbumsAsync(EventsMailRequest eventsmailRequest, List<Album> albums);
    }
}
