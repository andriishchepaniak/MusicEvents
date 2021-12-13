using Core.EmailService;
using Core.Jobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IMailService _mailservice;
        public JobsController(IMailService mailservice)
        {
            _mailservice = mailservice;
        }
        [Route("sendevents")]
        [HttpGet]
        public IActionResult SendEvents()
        {
            JobService.SendEventsJob();
            return Ok();
        }
        [Route("sendevalbums")]
        [HttpGet]
        public IActionResult SendAlbums()
        {
            JobService.SendAlbumsJob();
            return Ok();
        }
        [Route("refreshEvents")]
        [HttpGet]
        public IActionResult RefreshEvents()
        {
            JobService.RefreshEventsJob();
            return Ok();
        }
        [Route("refreshUserEvents")]
        [HttpGet]
        public IActionResult RefreshUserEvents()
        {
            JobService.RefreshUserEventsJob();
            return Ok();
        }
        [Route("getcurrentdirectory")]
        [HttpGet]
        public IActionResult GetCurrentDirectory()
        {
            return Ok(_mailservice.GetCurrentDirectory());
        }
    }
}
