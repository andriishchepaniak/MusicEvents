using AutoMapper;
using Core.Interfaces;
using Models.Entities;
using Quartz;
using SongkickAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs
{
    public class AddArtistEventsJob : IJob
    {
        IEventService _eventService;
        public AddArtistEventsJob(IEventService eventService)
        {
            _eventService = eventService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;

            JobDataMap dataMap = context.JobDetail.JobDataMap;

            int artistId = dataMap.GetInt("artistId");

            //var eventsCount = await _eventServiceApi.GetEventsCountByArtist(artistId);

            var e = new Event()
            {
                Id = 1,
                ArtistApiId = artistId,
                Date = DateTime.Now,
                DisplayName = "some name"
            };

            await _eventService.Add(e);
        }
    }
}
