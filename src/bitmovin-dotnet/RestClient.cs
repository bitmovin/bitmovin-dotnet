using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using com.bitmovin.Api.Exception;
using com.bitmovin.Api.Rest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Task = System.Threading.Tasks.Task;

namespace com.bitmovin.Api
{
    public class RestClient
    {
        private readonly Uri _apiUrl;
        private readonly string _apiKey;
        private readonly HttpClient _client;
        private readonly JsonSerializer _serializer;

        public RestClient(string apiKey, string apiUrl)
        {
            _apiUrl = new Uri(apiUrl);
            _apiKey = apiKey;

            _client = new HttpClient();
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            _client.DefaultRequestHeaders.TryAddWithoutValidation("X-Api-Key", _apiKey);
            _client.DefaultRequestHeaders.TryAddWithoutValidation("X-Api-Client", "bitmovin-dotnet");
            _client.DefaultRequestHeaders.TryAddWithoutValidation("X-Api-Client-Version", "1.0.11");

            _serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

#if !NET_40
        public async Task<T> PostAsync<T>(string url, T jsonObject)
        {
            var uri = new Uri(_apiUrl, url);
            var json = JObject.FromObject(jsonObject, _serializer).ToString();
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new BitmovinApiException(response);
            }
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseEnvelope<T>>(result).Data.Result;
        }

        public async Task<string> PostAndGetIdAsync<T>(string url, T jsonObject)
        {
            var uri = new Uri(_apiUrl, url);
            var json = JObject.FromObject(jsonObject, _serializer).ToString();
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new BitmovinApiException(response);
            }
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseEnvelope<IdWrapper>>(result).Data.Result.Id;
        }

        public async Task<string> PostAndGetIdAsync(string url)
        {
            var uri = new Uri(_apiUrl, url);
            var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new BitmovinApiException(response);
            }
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseEnvelope<IdWrapper>>(result).Data.Result.Id;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var uri = new Uri(_apiUrl, url);
            var response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                throw new BitmovinApiException(response);
            }
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseEnvelope<T>>(result).Data.Result;
        }

        public async Task<Dictionary<string, object>> GetCustomDataAsync(string url)
        {
            var uri = new Uri(_apiUrl, url);
            var response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                throw new BitmovinApiException(response);
            }
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
        }

        public async Task<List<T>> GetListAsync<T>(string url)
        {
            var uri = new Uri(_apiUrl, url);
            var response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                throw new BitmovinApiException(response);
            }
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PaginationResponse<T>>(result).Data.Result.Items;
        }

        public async Task<List<T>> GetAllIterativeAsync<T>(string url)
        {
            var uri = new Uri(_apiUrl, url);
            return await GetAllIterativeAsync<T>(uri);
        }

        private async Task<List<T>> GetAllIterativeAsync<T>(Uri uri)
        {
            var response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                throw new BitmovinApiException(response);
            }
            var result = await response.Content.ReadAsStringAsync();
            var paginationData = JsonConvert.DeserializeObject<PaginationResponse<T>>(result).Data;
            var items = paginationData.Result.Items;
            if (items.Count > 0)
            {
                var next = new Uri(paginationData.Result.Next);
                return items.Concat(await GetAllIterativeAsync<T>(next)).ToList();
            }
            return items;
        }

        public async Task DeleteAsync(string url)
        {
            var uri = new Uri(_apiUrl, url);
            var response = await _client.DeleteAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                throw new BitmovinApiException(response);
            }
        }

#endif

        public T Post<T>(string url, T jsonObject)
        {
            var uri = new Uri(_apiUrl, url);
            var json = JObject.FromObject(jsonObject, _serializer).ToString();
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = _client.PostAsync(uri, content).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new BitmovinApiException(response);
            }
            var result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<ResponseEnvelope<T>>(result).Data.Result;
        }

        public string PostAndGetId<T>(string url, T jsonObject)
        {
            var uri = new Uri(_apiUrl, url);
            var json = JObject.FromObject(jsonObject, _serializer).ToString();
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = _client.PostAsync(uri, content).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new BitmovinApiException(response);
            }
            var result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<ResponseEnvelope<IdWrapper>>(result).Data.Result.Id;
        }

        public string PostAndGetId(string url)
        {
            var uri = new Uri(_apiUrl, url);
            var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
            var response = _client.PostAsync(uri, content).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new BitmovinApiException(response);
            }
            var result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<ResponseEnvelope<IdWrapper>>(result).Data.Result.Id;
        }

        public T Get<T>(string url)
        {
            var uri = new Uri(_apiUrl, url);
            var response = _client.GetAsync(uri).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new BitmovinApiException(response);
            }
            var result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<ResponseEnvelope<T>>(result).Data.Result;
        }

        public Dictionary<string, object> GetCustomData(string url)
        {
            var uri = new Uri(_apiUrl, url);
            var response = _client.GetAsync(uri).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new BitmovinApiException(response);
            }
            var result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
        }

        public List<T> GetList<T>(string url)
        {
            var uri = new Uri(_apiUrl, url);
            var response = _client.GetAsync(uri).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new BitmovinApiException(response);
            }
            var result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<PaginationResponse<T>>(result).Data.Result.Items;
        }

        public List<T> GetAllIterative<T>(string url)
        {
            var uri = new Uri(_apiUrl, url);
            return GetAllIterative<T>(uri);
        }

        private List<T> GetAllIterative<T>(Uri uri)
        {
            var response = _client.GetAsync(uri).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new BitmovinApiException(response);
            }
            var result = response.Content.ReadAsStringAsync().Result;
            var paginationData = JsonConvert.DeserializeObject<PaginationResponse<T>>(result).Data;
            var items = paginationData.Result.Items;
            if (items.Count > 0)
            {
                var next = new Uri(paginationData.Result.Next);
                return items.Concat(GetAllIterative<T>(next)).ToList();
            }
            return items;
        }

        public void Delete(string url)
        {
            var uri = new Uri(_apiUrl, url);
            var response = _client.DeleteAsync(uri).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new BitmovinApiException(response);
            }
        }
    }
}