using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Entities;
using MusicEvents.Controllers;
using SongkickAPI.Interfaces;
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
        [Route("[action]/{artistId}")]
        [HttpGet()]
        public async Task<IActionResult> GetArtistsEvents(int artistId, int page = 1)
        {
            return await ExecuteAction(async () =>
            {
                return await _eventServiceApi.GetArtistsUpcomingEvents(artistId, page);
            });
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
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            return await ExecuteAction(async () => await _eventServiceApi.EventDetails(id));
        }
        [Route("getAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return await ExecuteAction(async () => await _eventService.GetAll());
        }
        [Route("addEvent")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Event e)
        {
            return await ExecuteAction(async () =>
            {
                return await _eventService.Add(e);
            });
        }
        [Route("addEvents")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<Event> events)
        {
            return await ExecuteAction(async () =>
            {
                return await _eventService.AddRange(events);
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Event e)
        {
            return await ExecuteAction(async () =>
            {
                return await _eventService.Update(e);
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await ExecuteAction(async () =>
            {
                return await _eventService.Delete(id);
            });
        }
        [Route("deleteAll")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            return await ExecuteAction(async () =>
            {
                return await _eventService.DeleteAll();
            });
        }
    }
}
