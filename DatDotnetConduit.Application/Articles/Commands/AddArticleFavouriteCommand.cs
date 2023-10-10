using DatDotnetConduit.Infrasturcture.Auth;
using DatDotnetConduit.Infrasturcture;
using MediatR;
using DatDotnetConduit.Application.Users.Common;
using DatDotnetConduit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DatDotnetConduit.Application.Articles.Common;
using DatDotnetConduit.Infrasturcture.Exceptions;
using System.Net;

namespace DatDotnetConduit.Application.Common.Articles.Commands
{
    public class AddArticleFavouriteCommand : IRequest<ArticleDTO>
    {
        public string Slug { get; init; }
    }
    public class AddArticleFavouriteCommandHandler : IRequestHandler<AddArticleFavouriteCommand, ArticleDTO>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ICurrentUser _currentUserService;
        public AddArticleFavouriteCommandHandler(MainDbContext mainDbContext, ICurrentUser currentUserService)
        {
            _mainDbContext = mainDbContext;
            _currentUserService = currentUserService;

        }
        public async Task<ArticleDTO> Handle(AddArticleFavouriteCommand request, CancellationToken cancellationToken)
        {
            var article = await _mainDbContext.Articles.Include(x => x.Author).Where(x => x.Slug == request.Slug && x.AuthorId == _currentUserService.Id).FirstOrDefaultAsync(cancellationToken);
            if (article.Slug == null)
            {
                throw new RestException(HttpStatusCode.NotFound,$"{request.Slug} is not exits");
            }
            var favourite = new Favourite
            {
                UserId = _currentUserService.Id.Value,
                ArticleId = article.Id
            };
            _mainDbContext.Favourites.Add(favourite);
            await _mainDbContext.SaveChangesAsync(cancellationToken);
            return new ArticleDTO
            {
                Description = article.Description,
                Title = article.Title,
                ContentArticle = article.Content,
                Tags = article.Tags,
                Id = article.Id,
                CreatedAt = article.CreatedAt,
                UpdatedAt = article.UpdatedAt,
                Favorited = true,
                FavoritesCount = article.Favorites.Count(),
                Author = new UserDto
                {
                    Username = article.Author.Username,
                    Image = article.Author.Image,
                    Description = article.Author.Description,
                    Email = article.Author.Email,

                },
                Slug = article.Slug,

            };
        }
    }
}

