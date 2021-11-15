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
    public class EventServiceApi : SongkickServiceApi, IEventServiceApi
    {
        public EventServiceApi(IRestClient client, IOptions<SongkickApiSettings> options) 
            : base(client, options)
        {
            
        }
        public async Task<int> GetEventsCountByArtist(int artistId)
        {
            var request = new RestRequest($"artists/{artistId}/calendar.json?apikey={api_key}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);
            return GetTotalCount(response);
        } 
        public async Task<int> GetEventsCountByVenue(int venueId)
        {
            var request = new RestRequest($"venues/{venueId}/calendar.json?apikey={api_key}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);
            return GetTotalCount(response);
        } 
        public async Task<int> GetEventsCountByCity(int metroAreaId)
        {
            var request = new RestRequest($"metro_areas/{metroAreaId}/calendar.json?apikey={api_key}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);
            return GetTotalCount(response);
        }
        public async Task<IEnumerable<EventApi>> GetArtistsUpcomingEvents(int artistId, int page=1)
        {
            var request = new RestRequest($"artists/{artistId}/calendar.json?apikey={api_key}&page={page}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);

            var events = ParseResult(response)["event"].ToObject<List<EventApi>>();
            return events;
        }
        
        public async Task<IEnumerable<EventApi>> GetVenuesUpcomingEvents(int venueId, int page=1)
        {
            var request = new RestRequest
                ($"venues/{venueId}/calendar.json?apikey={api_key}&page={page}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);

            var events = ParseResult(response)["event"].ToObject<List<EventApi>>();
            return events;
        }

        public async Task<IEnumerable<EventApi>> GetMetroUpcomingEvents(int metroAreaId, int page=1)
        {
            var request = new RestRequest
                ($"metro_areas/{metroAreaId}/calendar.json?apikey={api_key}&page={page}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);

            var events = ParseResult(response)["event"].ToObject<List<EventApi>>();
            return events;
        }

        public async Task<EventApi> EventDetails(int eventId)
        {
            var request = new RestRequest($"events/{eventId}.json?apikey={api_key}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);

            var Event = ParseResult(response)["event"].ToObject<EventApi>();
            return Event;
        }

    }
}
