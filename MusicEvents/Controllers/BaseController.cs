using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TParentClass> : ControllerBase
    {
        protected ILogger<TParentClass> Logger => _logger;
        ILogger<TParentClass> _logger;
        public BaseController(ILogger<TParentClass> logger)
        {
            _logger = logger;
        }
        protected async Task<IActionResult> ExecuteAction<TResult>(Func<Task<TResult>> action)
        {
            try
            {
                var result = await action();
                return Ok(result);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return BadRequest();
            }
        }
    }
}
