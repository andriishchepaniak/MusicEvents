using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Entities;
using SongkickAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : BaseController<CitiesController>
    {
        private readonly ICityService _cityService;
        private readonly ILocationServiceApi _locationServiceApi;
        public CitiesController(
            ICityService cityService,
            ILocationServiceApi locationServiceApi,
            ILogger<CitiesController> logger) : base(logger)
        {
            _cityService = cityService;
            _locationServiceApi = locationServiceApi;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await ExecuteAction(async () =>
            {
                return await _cityService.GetAll();
            });
        }
        [Route("[action]/{cityName}")]
        [HttpGet]
        public async Task<IActionResult> GetCity(string cityName)
        {
            return await ExecuteAction(async () =>
            {
                return await _locationServiceApi.GetByName(cityName);
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await ExecuteAction(async () =>
            {
                return await _cityService.GetById(id);
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] City value)
        {
            return await ExecuteAction(async () =>
            {
                return await _cityService.Add(value);
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] City value)
        {
            return await ExecuteAction(async () =>
            {
                return await _cityService.Update(value);
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await ExecuteAction(async () =>
            {
                return await _cityService.Delete(id);
            });
        }
        [Route("deleteAll")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            return await ExecuteAction(async () =>
            {
                return await _cityService.DeleteAll();
            });
        }
    }
}
