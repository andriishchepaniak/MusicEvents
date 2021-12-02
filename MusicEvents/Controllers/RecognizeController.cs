using AudDApi.Interfaces;
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
    public class RecognizeController : ControllerBase
    {
        private readonly IRecognitionService _recognitionService;
        public RecognizeController(IRecognitionService recognitionService)
        {
            _recognitionService = recognitionService;
        }
        [HttpPost("file")]
        public async Task<IActionResult> RecognizeTrack(IFormFile file)
        {
            return Ok(await _recognitionService.Recognize(file));
        }
        [HttpPost("url")]
        public async Task<IActionResult> RecognizeTrack(string url)
        {
            return Ok(await _recognitionService.Recognize(url));
        }
    }
}
