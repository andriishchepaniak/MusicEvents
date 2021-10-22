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

        public async Task<IEnumerable<EventApi>> GetArtistsUpcomingEvents(int artistId)
        {
            var request = new RestRequest($"artists/{artistId}/calendar.json?apikey={api_key}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);

            JArray eventsArr = (JArray)ParseResult(response)["event"];

            var events = eventsArr.ToObject<List<EventApi>>();
            return events;
        }
        
        public async Task<IEnumerable<EventApi>> GetVenuesUpcomingEvents(int venueId)
        {
            var request = new RestRequest($"venues/{venueId}/calendar.json?apikey={api_key}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);

            JArray eventsArr = (JArray)ParseResult(response)["event"];

            var events = eventsArr.ToObject<List<EventApi>>();
            return events;
        }

        public async Task<IEnumerable<EventApi>> GetMetroUpcomingEvents(int metro_areId)
        {
            var request = new RestRequest($"metro_areas/{metro_areId}/calendar.json?apikey={api_key}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);

            JArray eventsArr = (JArray)ParseResult(response)["event"];

            var events = eventsArr.ToObject<List<EventApi>>();
            return events;
        }

        public async Task<EventApi> EventDetails(int eventId)
        {
            var request = new RestRequest($"events/{eventId}.json?apikey={api_key}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);

            JObject eventsArr = (JObject)ParseResult(response)["event"];

            var Event = eventsArr.ToObject<EventApi>();
            return Event;
        }

    }
}
