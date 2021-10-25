using Core.DTO;
using Core.Interfaces;
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
        public async Task<IActionResult> GetArtistsEvents(int artistId)
        {
            return await ExecuteAction(async () =>
            {
                return await _eventServiceApi.GetArtistsUpcomingEvents(artistId);
            });
        }
        [Route("[action]/{venueId}")]
        [HttpGet()]
        public async Task<IActionResult> GetVenuesEvents(int venueId)
        {
            return await ExecuteAction(async () =>
            {
                return await _eventServiceApi.GetVenuesUpcomingEvents(venueId);
            });
        }
        [Route("[action]/{metroId}")]
        [HttpGet()]
        public async Task<IActionResult> GetMetroEvents(int metroId)
        {
            return await ExecuteAction(async () =>
            {
                return await _eventServiceApi.GetMetroUpcomingEvents(metroId);
            });
        }
        [Route("[action]/{userId}")]
        [HttpGet()]
        public async Task<IActionResult> GetUsersArtistsEvents(int userId)
        {
            return await ExecuteAction(async () =>
            {
                return await _eventService.GetArtistEventsByUserId(userId);
            });
        }
        [Route("[action]/{userId}")]
        [HttpGet()]
        public async Task<IActionResult> GetUsersCitiesEvents(int userId)
        {
            return await ExecuteAction(async () =>
            {
                return await _eventService.GetCityEventsByUserId(userId);
            });
        }
        [Route("[action]/{userId}")]
        [HttpGet()]
        public async Task<IActionResult> GetUsersArtistsAndCitiesEvents(int userId)
        {
            return await ExecuteAction(async () =>
            {
                return await _eventService.GetArtistAndCityEventsByUserId(userId);
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
