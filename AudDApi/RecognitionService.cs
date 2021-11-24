using System;
using System.IO;
using System.Net.Http;

namespace AudDApi
{
    public class RecognitionService
    {
        private readonly HttpClient _httpClient;
        public RecognitionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public void Recognize(string token, File file);
    }
}
