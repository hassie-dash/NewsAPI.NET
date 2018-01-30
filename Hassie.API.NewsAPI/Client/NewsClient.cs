using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hassie.NET.API.NewsAPI.API.v2;
using Hassie.NET.API.NewsAPI.Models;
using System.Net.Http;
using Hassie.NET.API.NewsAPI.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Hassie.NET.API.NewsAPI.Client
{
    internal class NewsClient : INewsClient
    {
        private readonly string apiKey;

        public NewsClient(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<INewsArticles> GetTopHeadlines(Category category)
        {
            string queryURL = $"top-headlines?category={category.ToString().ToLower()}";
            return await DownloadNewsArticles(queryURL);
        }

        public async Task<INewsArticles> GetTopHeadlines(Category category, Country country)
        {
            string queryURL = $"top-headlines?category={category.ToString().ToLower()}&country={country.ToString().ToLower()}";
            return await DownloadNewsArticles(queryURL);
        }

        public async Task<INewsArticles> GetTopHeadlines(Category category, string query)
        {
            query = query.Replace(" ", "%20");
            string queryURL = $"top-headlines?category={category.ToString().ToLower()}&q={query}";
            return await DownloadNewsArticles(queryURL);
        }

        public async Task<INewsArticles> GetTopHeadlines(Category category, Country country, string query)
        {
            query = query.Replace(" ", "%20");
            string queryURL = $"top-headlines?category={category.ToString().ToLower()}&country={country.ToString().ToLower()}&q={query}";
            return await DownloadNewsArticles(queryURL);
        }

        public async Task<INewsArticles> GetTopHeadlines(Country country)
        {
            string queryURL = $"top-headlines?country={country.ToString().ToLower()}";
            return await DownloadNewsArticles(queryURL);
        }

        public async Task<INewsArticles> GetTopHeadlines(Country country, string query)
        {
            query = query.Replace(" ", "%20");
            string queryURL = $"top-headlines?country={country.ToString().ToLower()}&q={query}";
            return await DownloadNewsArticles(queryURL);
        }

        public async Task<INewsArticles> GetTopHeadlines(string query)
        {
            query = query.Replace(" ", "%20");
            string queryURL = $"top-headlines?q={query}";
            return await DownloadNewsArticles(queryURL);
        }

        public async Task<INewsArticles> GetTopHeadlines(Source source)
        {
            string sourceString = source.ToString().ToLower().Replace('_', '-');
            string queryURL = $"top-headlines?source={sourceString}";
            return await DownloadNewsArticles(queryURL);
        }

        public async Task<INewsArticles> GetTopHeadlines(Source source, string query)
        {
            query = query.Replace(" ", "%20");
            string sourceString = source.ToString().ToLower().Replace('_', '-');
            string queryURL = $"top-headlines?source={sourceString}&q={query}";
            return await DownloadNewsArticles(queryURL);
        }

        public async Task<INewsArticles> GetTopHeadlines(params Source[] sources)
        {
            string sourcesString = null;
            for(int i = 0; i < sources.Length; i++)
            {
                if (sourcesString == null)
                {
                    sourcesString = sources[i].ToString().ToLower().Replace('_', '-');
                }
                else
                {
                    sourcesString = $"{sourcesString},{sources[i].ToString().ToLower().Replace('_', '-')}";
                }
            }
            string queryURL = $"top-headlines?source={sourcesString}";
            return await DownloadNewsArticles(queryURL);
        }

        public async Task<INewsArticles> GetTopHeadlines(string query, params Source[] sources)
        {
            string sourcesString = null;
            for (int i = 0; i < sources.Length; i++)
            {
                if (sourcesString == null)
                {
                    sourcesString = sources[i].ToString().ToLower().Replace('_', '-');
                }
                else
                {
                    sourcesString = String.Join(",", sourcesString, sources.ToString().ToLower().Replace('_', '-'));
                }
            }
            query = query.Replace(" ", "%20");
            string queryURL = $"top-headlines?source={sourcesString}&q={query}";
            return await DownloadNewsArticles(queryURL);
        }

        public async Task<INewsArticles> DownloadNewsArticles(string query)
        {
            string url = "https://newsapi.org/v2/" + $"{query}&apiKey={apiKey}";
            try
            {
                using(HttpClient httpClient = new HttpClient())
                {
                    // Get response from API.
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    // Check status code.
                    if (response.IsSuccessStatusCode)
                    {
                        // Success, parse json.
                        JObject json = JObject.Parse(await response.Content.ReadAsStringAsync());
                        return new NewsArticles(json);
                    }
                    else
                    {
                        // Parse error json and throw exception.
                        JObject json = JObject.Parse(await response.Content.ReadAsStringAsync());
                        throw new NewsHTTPException($"News API HTTP Exception - {json["code"]}: {json["message"]}");
                    }
                }
            }
            catch (HttpRequestException e1)
            {
                throw new NewsHTTPException("News API HTTP Exception - Failed to download JSON", e1);
            }
            catch (JsonException e2)
            {
                throw new NewsJSONException("News API JSON Exception - Failed to parse JSON", e2);
            }
        }
    }
}
