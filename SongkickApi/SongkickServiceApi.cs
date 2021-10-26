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
            JObject joResponse = JObject.Parse(response.Content);
            JObject resPageObject = (JObject)joResponse["resultsPage"];
            int totalEntries = (int)resPageObject["totalEntries"];
            //JObject resultObj = (JObject)resPageObject["results"];

            return totalEntries;
        }

        public JObject ParseResult(IRestResponse response)
        {
            JObject joResponse = JObject.Parse(response.Content);
            JObject resPageObject = (JObject)joResponse["resultsPage"];
            JObject resultObj = (JObject)resPageObject["results"];

            return resultObj;
        }
    }
}
