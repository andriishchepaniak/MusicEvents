using Core.DTO;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicEventsMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitySubsController : ControllerBase
    {
        private readonly ICitySubscriptionService _citySubService;
        public CitySubsController(ICitySubscriptionService citySubService)
        {
            _citySubService = citySubService;
        }
        // GET: api/<EventSubsController>
        [HttpGet]
        public async Task<IEnumerable<CitySubscriptionDTO>> Get()
        {
            return await _citySubService.GetAll();
        }

        // POST api/<EventSubsController>
        [HttpPost]
        public async Task<CitySubscriptionDTO> Post([FromBody] CitySubscriptionDTO value)
        {
            await _citySubService.Add(value);
            return value;
        }

        // PUT api/<EventSubsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EventSubsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _citySubService.Delete(id);
        }
    }
}
