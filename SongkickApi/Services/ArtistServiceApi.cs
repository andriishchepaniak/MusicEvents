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
    public class ArtistServiceApi : SongkickServiceApi, IArtistServiceApi
    {
        public ArtistServiceApi(IRestClient client, IOptions<SongkickApiSettings> options) 
            : base(client, options)
        {

        }

        public async Task<ArtistApi> GetArtistDetails(int artistId)
        {
            var request = new RestRequest($"artists/{artistId}.json?apikey={api_key}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);

            var artist = ParseResult(response)["artist"].ToObject<ArtistApi>();
            return artist;
        }

        public async Task<IEnumerable<ArtistApi>> GetArtistsByName(string artistName)
        {
            var request = new RestRequest($"search/artists.json?apikey={api_key}&query={artistName}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);

            var artists = ParseResult(response)["artist"].ToObject<List<ArtistApi>>();
            return artists;
        }
        public async Task<IEnumerable<ArtistApi>> GetSimilarArtists(int artistId)
        {
            var request = new RestRequest($"artists/{artistId}/similar_artists.json?apikey={api_key}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);

            var artists = ParseResult(response)["artist"].ToObject<List<ArtistApi>>();
            return artists;
        }
    }
}
