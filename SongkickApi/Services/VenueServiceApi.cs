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
    public class VenueServiceApi : SongkickServiceApi, IVenueServiceApi
    {
        public VenueServiceApi(IRestClient client, IOptions<SongkickApiSettings> options) 
            : base(client, options)
        {

        }
        public async Task<IEnumerable<Venue>> GetVenuesByName(string venueName)
        {
            var request = new RestRequest($"search/venues.json?apikey={api_key}&query={venueName}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);

            var venues = ParseResult(response)["venue"].ToObject<List<Venue>>();
            return venues;
        }
        public async Task<Venue> GetVenueDetails(int venueId)
        {
            var request = new RestRequest($"venues/{venueId}.json?apikey={api_key}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);

            var venue = ParseResult(response)["venue"].ToObject<Venue>();
            return venue;
        }
    }
}
