using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Interfaces
{
    public interface ISpotifyAccountService
    {
        Task<string> GetAccessToken();
    }
}
