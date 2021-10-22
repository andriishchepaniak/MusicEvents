using Microsoft.Extensions.Options;
using Models.SongkickEntities;
using Newtonsoft.Json.Linq;
using RestSharp;
using SongkickAPI.Interfaces;
using SongkickAPI.Settings;
using SongkickEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SongkickAPI.Services
{
    public class LocationServiceApi : SongkickServiceApi, ILocationServiceApi
    {
        public LocationServiceApi(IRestClient client, IOptions<SongkickApiSettings> options) 
            : base(client, options)
        {

        }

        public async Task<IEnumerable<LocationCity>> GetByName(string locationName)
        {
            var request = new RestRequest($"search/locations.json?apikey={api_key}&query={locationName}", Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);

            JArray locationsArr = (JArray)ParseResult(response)["location"];

            var locations = locationsArr.ToObject<List<LocationCity>>();
            return locations;
        }
    }
}
