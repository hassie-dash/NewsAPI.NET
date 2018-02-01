using Hassie.NET.API.NewsAPI.API.v2;
using Hassie.NET.API.NewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hassie.NET.API.NewsAPI.Client
{
    public interface INewsClient
    {
        /// <summary>
        /// Returns top news articles for the provided category.
        /// </summary>
        /// <param name="category">The category to retrieve news from.</param>
        /// <exception cref="Exceptions.NewsHTTPException"></exception>
        /// <exception cref="Exceptions.NewsJSONException"></exception>
        Task<INewsArticles> GetTopHeadlines(Category category);

        /// <summary>
        /// Returns top news articles for the provided category and country.
        /// </summary>
        /// <param name="category">The category to retrieve news from.</param>
        /// <param name="country">The country to retrieve news from.</param>
        /// <exception cref="Exceptions.NewsHTTPException"></exception>
        /// <exception cref="Exceptions.NewsJSONException"></exception>
        Task<INewsArticles> GetTopHeadlines(Category category, Country country);

        /// <summary>
        /// Returns top news articles for the provided category with a search query.
        /// </summary>
        /// <param name="category">The category to retrieve news from.</param>
        /// <param name="query">The topic to retrieve news about.</param>
        /// <exception cref="Exceptions.NewsHTTPException"></exception>
        /// <exception cref="Exceptions.NewsJSONException"></exception>
        Task<INewsArticles> GetTopHeadlines(Category category, string query);

        /// <summary>
        /// Returns top news articles for the provided category and country with a search query.
        /// </summary>
        /// <param name="category">The category to retrieve news from.</param>
        /// <param name="country">The country to retrieve news from.</param>
        /// <param name="query">The topic to retrieve news about.</param>
        /// <exception cref="Exceptions.NewsHTTPException"></exception>
        /// <exception cref="Exceptions.NewsJSONException"></exception>
        Task<INewsArticles> GetTopHeadlines(Category category, Country country, string query);

        /// <summary>
        /// Returns top news articles for the provided country.
        /// </summary>
        /// <param name="country">The country to retrieve news from.</param>
        /// <exception cref="Exceptions.NewsHTTPException"></exception>
        /// <exception cref="Exceptions.NewsJSONException"></exception>
        Task<INewsArticles> GetTopHeadlines(Country country);

        /// <summary>
        /// Returns top news articles for the provided country and search query.
        /// </summary>
        /// <param name="country">The country to retrieve news from.</param>
        /// <param name="query">The topic to retrieve news about.</param>
        /// <exception cref="Exceptions.NewsHTTPException"></exception>
        /// <exception cref="Exceptions.NewsJSONException"></exception>
        Task<INewsArticles> GetTopHeadlines(Country country, string query);

        /// <summary>
        /// Returns top news articles for a search query.
        /// </summary>
        /// <param name="query">The topic to retrieve news about.</param>
        /// <exception cref="Exceptions.NewsHTTPException"></exception>
        /// <exception cref="Exceptions.NewsJSONException"></exception>
        Task<INewsArticles> GetTopHeadlines(string query);

        /// <summary>
        /// Returns top news articles for the provided source.
        /// </summary>
        /// <param name="source">The source to retrieve news articles from.</param>
        /// <exception cref="Exceptions.NewsHTTPException"></exception>
        /// <exception cref="Exceptions.NewsJSONException"></exception>
        Task<INewsArticles> GetTopHeadlines(Source source);

        /// <summary>
        /// Returns top news articles for the provided source with a search query.
        /// </summary>
        /// <param name="source">The source to retrieve news articles from.</param>
        /// <param name="query">The topic to retrieve news about.</param>
        /// <exception cref="Exceptions.NewsHTTPException"></exception>
        /// <exception cref="Exceptions.NewsJSONException"></exception>
        Task<INewsArticles> GetTopHeadlines(Source source, string query);

        /// <summary>
        /// Returns top news articles for the provided sources.
        /// </summary>
        /// <param name="sources">The sources to retrieve news articles from.</param>
        /// <exception cref="Exceptions.NewsHTTPException"></exception>
        /// <exception cref="Exceptions.NewsJSONException"></exception>
        Task<INewsArticles> GetTopHeadlines(params Source[] sources);

        /// <summary>
        /// Returns top news articles for the provided sources with a search query.
        /// </summary>
        /// <param name="query">The topic to retrieve news about.</param>
        /// <param name="sources">The sources to retrieve news articles from.</param>
        /// <exception cref="Exceptions.NewsHTTPException"></exception>
        /// <exception cref="Exceptions.NewsJSONException"></exception>
        Task<INewsArticles> GetTopHeadlines(string query, params Source[] sources);

        /// <summary>
        /// Returns all news sources.
        /// </summary>
        Task<INewsSources> GetNewsSources();

        /// <summary>
        /// Returns news sources using the provided query.
        /// </summary>
        /// <param name="query">The query to be provided with the request.</param>
        /// <returns></returns>
        Task<INewsSources> GetNewsSources(NewsSourcesBuilder query);
    }
}