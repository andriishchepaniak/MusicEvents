using Microsoft.Extensions.Options;
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
    public class SpotifyAccountService : ISpotifyAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly ClientSettings _clientSettings;
        public SpotifyAccountService(HttpClient httpClient, IOptions<ClientSettings> options)
        {
            _httpClient = httpClient;
            _clientSettings = new ClientSettings()
            {
                ClientId = options.Value.ClientId,
                ClientSecret = options.Value.ClientSecret,
                AccountBaseUrl = options.Value.AccountBaseUrl,
                BaseUrl = options.Value.BaseUrl
            };
            _httpClient.BaseAddress = new Uri(_clientSettings.AccountBaseUrl);
        }
        public async Task<string> GetAccessToken()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "token");

            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(
                    Encoding.UTF8.GetBytes($"{_clientSettings.ClientId}:{_clientSettings.ClientSecret}")));

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "client_credentials"}
            });

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();

            var result = await JsonSerializer.DeserializeAsync<AuthResult>(responseStream);

            return result.access_token;
        }
    }
}
