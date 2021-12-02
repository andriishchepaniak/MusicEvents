using Models.SpotifyEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Interfaces
{
    public interface ISpotifyTrackService
    {
        Task<IEnumerable<TrackSpotify>> GetTracksByName(string trackName, int limit = 10);
        Task<IEnumerable<TrackSpotify>> GetAlbumTracks(string albumId);
    }
}
