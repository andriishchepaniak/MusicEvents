using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicEventsMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //private readonly IUserRepository userRepository;
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IEnumerable<UserDTO>> Get(int offset=0, int count=2)
        {
            //return await _unitOfWork.UserRepository.GetAll();
            //return await _unitOfWork.UserRepository.GetAll(u => u.FirstName == "test");
            return await _userService.GetRange(offset, count);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<UserDTO> Get(int id)
        {
            return await _userService.GetById(id);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<UserDTO> Post([FromBody] UserDTO user)
        {
            await _userService.Add(user);
            return user;
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<UserDTO> Put(int id, [FromBody] UserDTO user)
        {
            await _userService.Update(user);
            return user;
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _userService.Delete(id);
        }
    }
}
