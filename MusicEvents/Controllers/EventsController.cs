using Core.DTO;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using SongkickAPI.Services;
using SongkickEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicEventsMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventServiceApi _eventServiceApi;
        private readonly IEventService _eventService;
        public EventsController(EventServiceApi service, IEventService eventService)
        {
            _eventServiceApi = service;
            _eventService = eventService;
        }
        // GET: api/<EventsController>
        [Route("[action]/{artistId}")]
        [HttpGet()]
        public async Task<IEnumerable<EventApi>> GetArtistsEvents(int artistId)
        {
            return await _eventServiceApi.GetArtistsUpcomingEvents(artistId);
            
        }
        [Route("[action]/{venueId}")]
        [HttpGet()]
        public async Task<IEnumerable<EventApi>> GetVenuesEvents(int venueId)
        {
            return await _eventServiceApi.GetVenuesUpcomingEvents(venueId);   
        }
        [Route("[action]/{metroId}")]
        [HttpGet()]
        public async Task<IEnumerable<EventApi>> GetMetroEvents(int metroId)
        {
            return await _eventServiceApi.GetMetroUpcomingEvents(metroId);
            
        }
        [Route("[action]/{userId}")]
        [HttpGet()]
        public async Task<IEnumerable<EventApi>> GetUsersArtistsEvents(int userId)
        {
            return await _eventService.GetArtistEventsByUserId(userId);
        }
        [Route("[action]/{userId}")]
        [HttpGet()]
        public async Task<IEnumerable<EventApi>> GetUsersCitiesEvents(int userId)
        {
            return await _eventService.GetCityEventsByUserId(userId);
        }
        
        // GET api/<EventsController>/5
        [HttpGet("{id}")]
        public async Task<EventApi> GetEventById(int id)
        {
            return await _eventServiceApi.EventDetails(id);
        }
    }
}
