using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Entities;
using MusicEvents.Controllers;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicEventsMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController<UsersController>
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService, ILogger<UsersController> logger) : base(logger)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int offset=0, int count=2)
        {
            return await ExecuteAction(async () =>
            {
                return await _userService.GetRange(offset, count);
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await ExecuteAction(async () =>
            {
                return await _userService.GetById(id);
            });
        }
        //[Authorize]
        [Route("{id}/artists")]
        [HttpGet]
        public async Task<IActionResult> GetArtists(int id)
        {
            return await ExecuteAction(async () =>
            {
                return await _userService.GetUserArtists(id);
            });
        }
        [Route("{id}/cities")]
        [HttpGet]
        public async Task<IActionResult> GetCities(int id)
        {
            return await ExecuteAction(async () =>
            {
                var user = await _userService.GetById(id);
                return user.Cities;
            });
        }
        
        [Route("{id}/events")]
        [HttpGet]
        public async Task<IActionResult> GetEvents(int id)
        {
            return await ExecuteAction(async () =>
            {
                return await _userService.GetUserEvents(id);
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            return await ExecuteAction(async () =>
            {
                await _userService.Add(user);
                return user;
            }); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            return await ExecuteAction(async () =>
            {
                await _userService.Update(user);
                return user;
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await ExecuteAction(async () =>
            {
                return await _userService.Delete(id);
            });
        }
    }
}
