using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Models.Entities;
using Models.SpotifyEntities;
using SongkickEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EmailService
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendAlbumsAsync(EventsMailRequest eventsmailRequest, List<Album> albums)
        {
            //throw new Exception();
            string htmlFilePath = "D:\\Progi\\C#\\Eleks\\MusicEvents\\MusicEvents\\wwwroot\\Tamplates\\AlbumsTemplates\\albumsNotificationTamplate.html";// Directory.GetCurrentDirectory() + "\\wwwroot\\Templates\\notificationTamplate.html";
            string listFilePath = "D:\\Progi\\C#\\Eleks\\MusicEvents\\MusicEvents\\wwwroot\\Tamplates\\AlbumsTemplates\\albumsDetails.html";// Directory.GetCurrentDirectory() + "\\wwwroot\\Templates\\notificationTamplate.html";
            StreamReader str1 = new StreamReader(htmlFilePath);
            StreamReader str2 = new StreamReader(listFilePath);
            string MailText = str1.ReadToEnd();
            string albumDetail = str2.ReadToEnd();
            str1.Close();
            str2.Close();
            var albumDetails = new List<string>();

            foreach (var album in albums)
            {
                var artists = "";
                foreach (var artist in album.artists.ToList())
                {
                    artists += artist.name + " & ";
                }
                albumDetails.Add(albumDetail
                    .Replace("[albumType]", album.album_type)
                    .Replace("[albumName]", album.name)
                    .Replace("[artist]", artists)
                    .Replace("[date]", album.release_date.ToString())
                    .Replace("[image]", album.images[1].url)
                    .Replace("[site]", album.uri));
            }

            MailText = MailText.Replace("[username]", eventsmailRequest.UserName);

            foreach (var i in albumDetails)
            {
                MailText = MailText.Replace("[details]", i + "[details]");
            }
            MailText = MailText.Replace("[details]", "");

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(eventsmailRequest.ToEmail));
            email.Subject = eventsmailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendEventsAsync(EventsMailRequest eventsmailRequest, List<EventApi> events)
        {
            string htmlFilePath = "D:\\Progi\\C#\\Eleks\\MusicEvents\\MusicEvents\\wwwroot\\Tamplates\\EventsTemplates\\eventsNotificationTamplate.html";// Directory.GetCurrentDirectory() + "\\wwwroot\\Templates\\notificationTamplate.html";
            string listFilePath = "D:\\Progi\\C#\\Eleks\\MusicEvents\\MusicEvents\\wwwroot\\Tamplates\\EventsTemplates\\eventDetails.html";// Directory.GetCurrentDirectory() + "\\wwwroot\\Templates\\notificationTamplate.html";
            StreamReader str1 = new StreamReader(htmlFilePath);
            StreamReader str2 = new StreamReader(listFilePath);
            string MailText = str1.ReadToEnd();
            string EventDetail = str2.ReadToEnd();
            str1.Close();
            str2.Close();
            var EventDetails = new List<string>();

            foreach (var e in events)
            {
                EventDetails.Add(EventDetail
                    .Replace("[eventName]", e.displayName)
                    .Replace("[artist]", e.performance[0].artist.displayName)
                    .Replace("[date]", e.start.date)
                    .Replace("[city]", e.location.city)
                    .Replace("[venue]", e.venue.displayName)
                    .Replace("[site]", e.uri));
            }

            MailText = MailText.Replace("[username]", eventsmailRequest.UserName);

            foreach (var i in EventDetails)
            {
                MailText = MailText.Replace("[details]", i + "[details]");
            }
            MailText = MailText.Replace("[details]", "");

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(eventsmailRequest.ToEmail));
            email.Subject = eventsmailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
