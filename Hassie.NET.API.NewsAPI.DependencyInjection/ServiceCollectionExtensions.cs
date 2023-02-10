using Hassie.NET.API.NewsAPI.Client;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hassie.NET.API.NewsAPI.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IHttpClientBuilder AddNewsApi(this IServiceCollection services, Func<ClientOptions, ClientOptions> options)
        {
            services.AddSingleton(x => options(new ClientOptions()));
            return services.AddHttpClient<INewsClient, NewsClient>();
        }
    }
}
