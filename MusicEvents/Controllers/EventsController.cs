using Core.DTO;
using Core.Interfaces;
using Core.Jobs;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Entities;
using MusicEvents.Controllers;
using SongkickAPI.Interfaces;
using SongkickAPI.Services;
using SongkickEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicEventsMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : BaseController<EventsController>
    {
        private readonly IEventServiceApi _eventServiceApi;
        private readonly IEventService _eventService;
        public EventsController(
            IEventServiceApi service,
            IEventService eventService,
            ILogger<EventsController> logger) : base(logger)
        {
            _eventServiceApi = service;
            _eventService = eventService;
        }
        // GET: api/<EventsController>
        [Route("[action]/{artistId}")]
        [HttpGet()]
        public async Task<IActionResult> GetArtistsEvents(int artistId, int page = 1)
        {
            AddArtistEventsScheduler.Start(artistId);
            return Ok(await _eventService.GetEventsByArtistId(artistId, page));
            //return await ExecuteAction(async () =>
            //{
            //    //return await _eventServiceApi.GetArtistsUpcomingEvents(artistId, page);
            //    return await _eventService.GetEventsByArtistId(artistId, page);
            //});
        }
        [Route("[action]/{venueId}")]
        [HttpGet()]
        public async Task<IActionResult> GetVenuesEvents(int venueId, int page = 1)
        {
            return await ExecuteAction(async () =>
            {
                return await _eventServiceApi.GetVenuesUpcomingEvents(venueId, page);
            });
        }
        [Route("[action]/{metroId}")]
        [HttpGet()]
        public async Task<IActionResult> GetMetroEvents(int metroId, int page = 1)
        {
            return await ExecuteAction(async () =>
            {
                return await _eventServiceApi.GetMetroUpcomingEvents(metroId, page);
            });
        }
        [Route("[action]/{userId}")]
        [HttpGet()]
        public async Task<IActionResult> GetUsersArtistsEvents(int userId, int page = 1)
        {
            return await ExecuteAction(async () =>
            {
                return await _eventService.GetArtistEventsByUserId(userId, page);
            });
        }
        [Route("[action]/{userId}")]
        [HttpGet()]
        public async Task<IActionResult> GetUsersCitiesEvents(int userId, int page = 1)
        {
            return await ExecuteAction(async () =>
            {
                return await _eventService.GetCityEventsByUserId(userId, page);
            });
        }
        [Route("[action]/{userId}")]
        [HttpGet()]
        public async Task<IActionResult> GetUsersArtistsAndCitiesEvents(int userId, int page = 1)
        {
            return await ExecuteAction(async () =>
            {
                return await _eventService.GetArtistAndCityEventsByUserId(userId, page);
            });
        }
        
        // GET api/<EventsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            return await ExecuteAction(async () => await _eventServiceApi.EventDetails(id));
        }
    }
}
