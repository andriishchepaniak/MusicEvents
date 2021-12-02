using AudDApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Models.AudDEntities;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace AudDApi
{
    public class RecognitionService : IRecognitionService
    {
        private readonly HttpClient _httpClient;
        private readonly AudDSettings settings;
        public RecognitionService(HttpClient httpClient, IOptions<AudDSettings> options)
        {
            _httpClient = httpClient;
            settings = new AudDSettings()
            {
                api_token = options.Value.api_token,
                base_url = options.Value.base_url
            };
            _httpClient.BaseAddress = new Uri(settings.base_url);
        }
        private MultipartFormDataContent GetFormData()
        {
            var form = new MultipartFormDataContent();
            form.Add(new StringContent(settings.api_token), "api_token");
            form.Add(new StringContent("recognize"), "method");
            form.Add(new StringContent("apple_music,spotify"), "return");

            return form;
        }
        public async Task<TrackAudD> Recognize(IFormFile file)
        {
            var form = GetFormData();
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                form.Add(new ByteArrayContent(fileBytes), "file", file.FileName);
            }

            var response = await _httpClient.PostAsync("", form);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JObject
                          .Parse(responseContent)["result"]
                          .ToObject<TrackAudD>();
            return result;
        }

        public async Task<TrackAudD> Recognize(string url)
        {
            var form = GetFormData();
            form.Add(new StringContent(url), "url");
            var response = await _httpClient.PostAsync("", form);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JObject
                          .Parse(responseContent)["result"]
                          .ToObject<TrackAudD>();
            return result;
        }
    }
}
