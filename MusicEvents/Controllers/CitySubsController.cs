using Core.DTO;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicEvents.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicEventsMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitySubsController : BaseController<CitySubsController>
    {
        private readonly ICitySubscriptionService _citySubService;
        public CitySubsController(ICitySubscriptionService citySubService, 
            ILogger<CitySubsController> logger) : base(logger)
        {
            _citySubService = citySubService;
        }
        // GET: api/<EventSubsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await ExecuteAction(async () =>
            {
                return await _citySubService.GetAll();
            });
        }

        // POST api/<EventSubsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CitySubscriptionDTO value)
        {
            return await ExecuteAction(async () =>
            {
                await _citySubService.Add(value);
                return value;
            });
        }

        // PUT api/<EventSubsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CitySubscriptionDTO value)
        {
            return await ExecuteAction(async () =>
            {
                await _citySubService.Update(value);
                return value;
            });
        }

        // DELETE api/<EventSubsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await ExecuteAction(async () =>
            {
                return await _citySubService.Delete(id);
            });
        }
    }
}
