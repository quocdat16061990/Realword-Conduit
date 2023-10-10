
using DatDotnetConduit.Application.Articles.Common;
using DatDotnetConduit.Application.Users.Common;
using DatDotnetConduit.Infrasturcture;
using DatDotnetConduit.Infrasturcture.Auth;
using DatDotnetConduit.Infrasturcture.LinQ;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace DatDotnetConduit.Application.Common.Articles.Queries
{
    public class ArticleGlobalQuery : PagingParamsDTO, IRequest<PagingDTO<ArticleDTO>>
    {
        public string Tag { get; init; }
        public string Author { get; init; }
        public string Favourited { get; init; }
    }

    internal class ArticleQueryHandler : IRequestHandler<ArticleGlobalQuery, PagingDTO<ArticleDTO>>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ICurrentUser _currentUser;

        public ArticleQueryHandler(MainDbContext mainDbContext, ICurrentUser currentUser)
        {
            _mainDbContext = mainDbContext;
            _currentUser = currentUser;
        }

        public async Task<PagingDTO<ArticleDTO>> Handle(ArticleGlobalQuery request, CancellationToken cancellationToken)
        {
            var query = _mainDbContext.Articles
                .WhereIf(!string.IsNullOrEmpty(request.Tag), x => x.Tags.Contains(request.Tag))
                .WhereIf(!string.IsNullOrEmpty(request.Author), x => x.Author.Username == request.Author)
                .WhereIf(!string.IsNullOrEmpty(request.Favourited), x => x.Favorites.Any(f => f.User.Username == request.Favourited));

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Select(x => new ArticleDTO
                {
                    ContentArticle = x.Content,
                    Description = x.Description,
                    Slug = x.Slug,
                    Title = x.Title,
                    Tags = x.Tags,
                    FavoritesCount = x.Favorites.Count(),
                    Favorited = x.Favorites.Any(f => f.UserId == _currentUser.Id),
                    Author = new UserDto
                    {
                        Description = x.Author.Description,
                        Email = x.Author.Email,
                        Image = x.Author.Image,
                        Username = x.Author.Username,
                        IsFollowed = x.Author.Followers.Any(f => f.FollowingId == _currentUser.Id)
                    }
                })
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToListAsync(cancellationToken);

            return new PagingDTO<ArticleDTO>(totalCount, items);
        }
    }

}