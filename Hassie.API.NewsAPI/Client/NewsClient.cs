using Hassie.NET.API.NewsAPI.API.v2;
using Hassie.NET.API.NewsAPI.Exceptions;
using Hassie.NET.API.NewsAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hassie.NET.API.NewsAPI.Client
{
    public class NewsClient : INewsClient
    {
        private readonly ClientOptions _clientOptions;
        private readonly HttpClient _httpClient;

        public NewsClient(ClientOptions clientOptions,
                          HttpClient httpClient)
        {
            _clientOptions = clientOptions;
            _httpClient = httpClient;

            httpClient.DefaultRequestHeaders.Add("User-Agent", _clientOptions.UserAgent);
        }

        public async Task<INewsArticles> GetEverything(EverythingBuilder query)
        {
            return new NewsArticles(await GetResponse(String.Concat(query.ToString(), "&apiKey=", _clientOptions.ApiKey)));
        }

        public async Task<INewsArticles> GetTopHeadlines(TopHeadlinesBuilder query)
        {
            return new NewsArticles(await GetResponse(String.Concat(query.ToString(), "&apiKey=", _clientOptions.ApiKey)));
        }

        public async Task<INewsSources> GetNewsSources()
        {
            return new NewsSources(await GetResponse(String.Concat(new NewsSourcesBuilder().Build().ToString(), "?apiKey=", _clientOptions.ApiKey)));
        }

        public async Task<INewsSources> GetNewsSources(NewsSourcesBuilder query)
        {
            return new NewsSources(await GetResponse(String.Concat(query.ToString(), "&apiKey=", _clientOptions.ApiKey)));
        }

        private async Task<JObject> GetResponse(string query)
        {
            try
            {
                // Get response from API.
                HttpResponseMessage response = await _httpClient.GetAsync(query);

                // Check status code.
                if (response.IsSuccessStatusCode)
                {
                    // Success, parse json.
                    return JObject.Parse(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    // Parse error json and throw exception.
                    JObject json = JObject.Parse(await response.Content.ReadAsStringAsync());
                    throw new NewsHTTPException($"News API HTTP Exception - {json["code"]}: {json["message"]}");
                }

            }
            catch (HttpRequestException e1)
            {
                throw new NewsHTTPException("News API HTTP Exception - Failed to get response", e1);
            }
            catch (JsonException e2)
            {
                throw new NewsJSONException("News API JSON Exception - Failed to parse response", e2);
            }
        }
    }
}