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
    public class UsersController : BaseController<UsersController>
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService, ILogger<UsersController> logger) : base(logger)
        {
            _userService = userService;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> Get(int offset=0, int count=2)
        {
            //var result = await _userService.GetRange(offset, count);
            //return result != null 
            //    ? Ok(result) 
            //    : BadRequest();
            return await ExecuteAction(async () =>
            {
                return await _userService.GetRange(offset, count);
            });
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            //var result = await _userService.GetById(id);
            //return result != null
            //    ? Ok(result)
            //    : BadRequest();
            return await ExecuteAction(async () =>
            {
                return await _userService.GetById(id);
            });
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO user)
        {
            return await ExecuteAction(async () =>
            {
                await _userService.Add(user);
                return user;
            }); 
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDTO user)
        {
            return await ExecuteAction(async () =>
            {
                await _userService.Update(user);
                return user;
            });
        }

        // DELETE api/<UsersController>/5
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
