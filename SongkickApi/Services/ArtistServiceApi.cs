using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using SongkickAPI.Interfaces;
using SongkickAPI.Settings;
using SongkickEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SongkickAPI.Services
{
    public class ArtistServiceApi : SongkickApi, IArtistServiceApi
    {
        public ArtistServiceApi(IRestClient client, IOptions<SongkickApiSettings> options) 
            : base(client, options)
        {

        }

        public async Task<Artist> GetArtistDetails(int artistId)
        {
            var request = new RestRequest($"artists/{artistId}.json?apikey={api_key}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);

            JObject eventsArr = (JObject)ParseResult(response)["artist"];

            var artist = eventsArr.ToObject<Artist>();
            return artist;
        }

        public async Task<IEnumerable<Artist>> GetArtistsByName(string artistName)
        {
            var request = new RestRequest($"search/artists.json?apikey={api_key}&query={artistName}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);

            JArray artistsArr = (JArray)ParseResult(response)["artist"];

            var artists = artistsArr.ToObject<List<Artist>>();
            return artists;
        }
        public async Task<IEnumerable<Artist>> GetSimilarArtists(int artistId)
        {
            var request = new RestRequest($"artists/{artistId}/similar_artists.json?apikey={api_key}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);

            JArray artistsArr = (JArray)ParseResult(response)["artist"];

            var artists = artistsArr.ToObject<List<Artist>>();
            return artists;
        }
    }
}
