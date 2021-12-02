using Microsoft.Extensions.Options;
using Models.SpotifyEntities;
using Newtonsoft.Json.Linq;
using SpotifyApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.Services
{
    public class SpotifyTrackService : ISpotifyTrackService
    {
        private readonly HttpClient _httpClient;
        private readonly ISpotifyAccountService _accountService;
        public SpotifyTrackService(HttpClient httpClient, IOptions<ClientSettings> options, ISpotifyAccountService accountService)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
            _accountService = accountService;
        }
        public async Task<IEnumerable<TrackSpotify>> GetTracksByName(string trackName, int limit = 10)
        {
            var token = await _accountService.GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"search?q={trackName}&type=track&limit={limit}");
            response.EnsureSuccessStatusCode();
            var responseStr = await response.Content.ReadAsStringAsync();

            var result = JObject.Parse(responseStr)["tracks"]["items"]
                                .ToObject<List<TrackSpotify>>();

            return result;
        }
        public async Task<IEnumerable<TrackSpotify>> GetAlbumTracks(string albumId)
        {
            var token = await _accountService.GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"albums/{albumId}/tracks");
            response.EnsureSuccessStatusCode();
            var responseStr = await response.Content.ReadAsStringAsync();

            var result = JObject.Parse(responseStr)["items"]
                                .ToObject<List<TrackSpotify>>();

            return result;
        }
    }
}
