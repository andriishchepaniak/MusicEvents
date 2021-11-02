using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Entities;
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
        public CitiesController(
            ICityService cityService,
            ILogger<CitiesController> logger) : base(logger)
        {
            _cityService = cityService;
        }
        // GET: api/<CitiesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await ExecuteAction(async () =>
            {
                return await _cityService.GetAll();
            });
        }

        // GET api/<CitiesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await ExecuteAction(async () =>
            {
                return await _cityService.GetById(id);
            });
        }

        // POST api/<CitiesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] City value)
        {
            return await ExecuteAction(async () =>
            {
                return await _cityService.Add(value);
            });
        }

        // PUT api/<CitiesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] City value)
        {
            return await ExecuteAction(async () =>
            {
                return await _cityService.Update(value);
            });
        }

        // DELETE api/<CitiesController>/5
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
