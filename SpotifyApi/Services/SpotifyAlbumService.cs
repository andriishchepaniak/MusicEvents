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
using System.Text.Json;
using System.Threading.Tasks;

namespace SpotifyApi.Services
{
    public class SpotifyAlbumService : ISpotifyAlbumService
    {
        private readonly HttpClient _httpClient;
        private readonly ISpotifyAccountService _spotifyAccountService;
        public SpotifyAlbumService(HttpClient httpClient, IOptions<ClientSettings> options, ISpotifyAccountService spotifyAccountService)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
            _spotifyAccountService = spotifyAccountService;
        }
        public async Task<Album> GetAlbumById(string albumId)
        {
            var token = await _spotifyAccountService.GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"albums/{albumId}");
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();

            var result = await JsonSerializer.DeserializeAsync<Album>(responseStream);

            return result;
        }
        public async Task<IEnumerable<Album>> GetAlbumsByArtistId(string artistId)
        {
            var token = await _spotifyAccountService.GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"artists/{artistId}/albums");
            response.EnsureSuccessStatusCode();
            var responseStr = await response.Content.ReadAsStringAsync();

            var result = JObject.Parse(responseStr)["items"]
                                .ToObject<List<Album>>();
                                //.Where(a => a.album_type=="album");
            return result;
        }

        
    }
}
