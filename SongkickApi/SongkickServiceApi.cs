using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RestSharp;
using SongkickAPI.Settings;
using System;

namespace SongkickAPI
{
    public class SongkickServiceApi : ISongkickApi
    {
        protected IRestClient _client;
        protected string api_key;

        public SongkickServiceApi(IRestClient client, IOptions<SongkickApiSettings> options)
        {
            _client = client;
            _client.BaseUrl = new Uri(options.Value.BaseUrl);
            api_key = options.Value.ApiKey;
        }

        public int GetTotalCount(IRestResponse response)
        {
            var totalEntries = JObject.Parse(response.Content)["resultsPage"]["totalEntries"].ToObject<int>();
            return totalEntries;
        }

        public JObject ParseResult(IRestResponse response)
        {
            var res = JObject.Parse(response.Content)["resultsPage"]["results"].ToObject<JObject>();
            return res;
        }
    }
}
