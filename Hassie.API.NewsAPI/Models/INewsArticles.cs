using System;
using System.Collections.Generic;
using System.Text;

namespace Hassie.NET.API.NewsAPI.Models
{
    public interface INewsArticles
    {
        List<INewsArticle> Articles { get; }
    }
}
