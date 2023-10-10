using DatDotnetConduit.Application.Users.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatDotnetConduit.Application.Articles.Common
{
    public class ArticleDTO
    {
        public Guid Id { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
        public bool Favorited { get; init; }
        public int FavoritesCount { get; init; }

        public string Title { get; init; }
        public string ContentArticle { get; init; }
        public string Description { get; init; }
        public List<string> Tags { get; init; }
        public string Slug { get; init; }
        public UserDto Author { get; init; }
    }
}
