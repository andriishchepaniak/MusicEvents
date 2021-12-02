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
    public class SpotifyArtistService : ISpotifyArtistService
    {
        private readonly HttpClient _httpClient;
        private readonly ISpotifyAccountService _spotifyAccountService;
        public SpotifyArtistService(HttpClient httpClient, IOptions<ClientSettings> options, ISpotifyAccountService spotifyAccountService)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
            _spotifyAccountService = spotifyAccountService;
        }
        public async Task<IEnumerable<ArtistSpotify>> GetArtistsByName(string artistName, int limit = 10)
        {
            var token = await _spotifyAccountService.GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"search?q={artistName}&type=artist&limit={limit}");
            response.EnsureSuccessStatusCode();
            var responseStr = await response.Content.ReadAsStringAsync();

            var result = JObject.Parse(responseStr)["artists"]["items"]
                                .ToObject<List<ArtistSpotify>>();

            return result;
        }

        public async Task<ArtistSpotify> GetFirstArtist(string artistName)
        {
            var result = await GetArtistsByName(artistName);
            return result.First();
        }
    }
}
