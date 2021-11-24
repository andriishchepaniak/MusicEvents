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
        public string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }
        private async Task SendMail(string mailText, EventsMailRequest eventsmailRequest)
        {
            mailText = mailText.Replace("[username]", eventsmailRequest.UserName);

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(eventsmailRequest.ToEmail));
            email.Subject = eventsmailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        public async Task SendAlbumsAsync(EventsMailRequest eventsmailRequest, List<Album> albums)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            currentDirectory = currentDirectory.Replace(@"\", @"\\");
            var htmlFilePath = currentDirectory + "\\wwwroot\\Tamplates\\AlbumsTemplates\\albumsNotificationTamplate.html";
            var listFilePath = currentDirectory + "\\wwwroot\\Tamplates\\AlbumsTemplates\\albumsDetails.html";
            StreamReader str1 = new StreamReader(htmlFilePath);
            StreamReader str2 = new StreamReader(listFilePath);
            var MailText = str1.ReadToEnd();
            var albumDetail = str2.ReadToEnd();
            str1.Close();
            str2.Close();
            var albumDetails = new List<string>();

            foreach (var album in albums)
            {
                var artistsNames = "";
                var artists = album.artists.ToList();
                for (var i = 0; i < artists.Count; i++)
                {
                    if(i == artists.Count - 1)
                    {
                        artistsNames += artists[i].name;
                        continue;
                    }
                    artistsNames += artists[i].name + " & ";
                }
                albumDetails.Add(albumDetail
                    .Replace("[albumType]", album.album_type)
                    .Replace("[albumName]", album.name)
                    .Replace("[artist]", artistsNames)
                    .Replace("[date]", album.release_date.ToString())
                    .Replace("[image]", album.images[1].url)
                    .Replace("[site]", album.uri));
            }

            foreach (var i in albumDetails)
            {
                MailText = MailText.Replace("[details]", i + "[details]");
            }
            MailText = MailText.Replace("[details]", "");

            await SendMail(MailText, eventsmailRequest);
        }

        public async Task SendEventsAsync(EventsMailRequest eventsmailRequest, List<EventApi> events)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            currentDirectory = currentDirectory.Replace(@"\", @"\\");
            var htmlFilePath = currentDirectory + "\\wwwroot\\Tamplates\\EventsTemplates\\eventsNotificationTamplate.html";
            var listFilePath = currentDirectory + "\\wwwroot\\Tamplates\\EventsTemplates\\eventDetails.html";
            StreamReader str1 = new StreamReader(htmlFilePath);
            StreamReader str2 = new StreamReader(listFilePath);
            var MailText = str1.ReadToEnd();
            var EventDetail = str2.ReadToEnd();
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

            foreach (var i in EventDetails)
            {
                MailText = MailText.Replace("[details]", i + "[details]");
            }
            MailText = MailText.Replace("[details]", "");

            await SendMail(MailText, eventsmailRequest);
        }
    }
}
