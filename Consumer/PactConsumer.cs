using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Consumer
{
    public class PactConsumer
    {
        private readonly string _baseUri;

        public PactConsumer(string baseUri)
        {
            _baseUri = baseUri;
        }

        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        public async Task<string> DoTheThingAsync(string id)
        {
            using (var client = new HttpClient {BaseAddress = new Uri(_baseUri) })
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, $"/api/theThing/{id}"))
                {
                    request.Headers.Add("Accept", "application/json");

                    using (var response = await client.SendAsync(request))
                    {
                        var body = await response.Content.ReadAsStringAsync();
                        var responseContent = JsonConvert.DeserializeObject<TheThing>(body, _jsonSettings);
                        return responseContent.Value;
                    }
                }
            }
        }

        public class TheThing
        {
            public string Value { get; set; }
        }
    }
}
