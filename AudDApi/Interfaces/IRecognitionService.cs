using Microsoft.AspNetCore.Http;
using Models.AudDEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudDApi.Interfaces
{
    public interface IRecognitionService
    {
        Task<TrackAudD> Recognize(IFormFile file);
        Task<TrackAudD> Recognize(string url);
    }
}
