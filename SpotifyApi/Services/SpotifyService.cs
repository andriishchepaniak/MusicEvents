using Newtonsoft.Json.Linq;
using SpotifyApi.Interfaces;
using SpotifyApi.Models;
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
    public class SpotifyService : ISpotifyService
    {
        private readonly HttpClient _httpClient;
        public SpotifyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Album> GetAlbumById(string albumId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"albums/{albumId}");
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();

            var result = await JsonSerializer.DeserializeAsync<Album>(responseStream);

            return result;
        }
        public async Task<IEnumerable<Album>> GetAlbumsByArtistId(string artistId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"artists/{artistId}/albums");
            response.EnsureSuccessStatusCode();
            var responseStr = await response.Content.ReadAsStringAsync();

            var result = JObject.Parse(responseStr)["items"]
                                .ToObject<List<Album>>();
                                //.Where(a => a.album_type=="album");
            return result;
        }

        public async Task<List<Artist>> GetArtistsByName(string artistName, string token, int limit=10)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"search?q={artistName}&type=artist&limit={limit}");
            response.EnsureSuccessStatusCode();
            var responseStr = await response.Content.ReadAsStringAsync();

            var result = JObject.Parse(responseStr)["artists"]["items"]
                                .ToObject<List<Artist>>();
            
            return result;
        }

        public async Task<Artist> GetFirstArtist(string artistName, string token)
        {
            var result = await GetArtistsByName(artistName, token);
            return result.First();
        }
    }
}
