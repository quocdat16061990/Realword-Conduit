using DatDotnetConduit.Application.Articles.Common;
using DatDotnetConduit.Infrasturcture.Auth;
using DatDotnetConduit.Infrasturcture;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatDotnetConduit.Application.Users.Common;
using DatDotnetConduit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DatDotnetConduit.Infrasturcture.Exceptions;
using System.Net;

namespace DatDotnetConduit.Application.Articles.Commands
{
    public class DeleteArticleFavouriteCommand : IRequest<ArticleDTO>
    {
        public string Slug { get; init; }
    }
    public class DeleteArticleFavouriteCommandHandler : IRequestHandler<DeleteArticleFavouriteCommand, ArticleDTO>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ICurrentUser _currentUserService;
        public DeleteArticleFavouriteCommandHandler(MainDbContext mainDbContext, ICurrentUser currentUserService)
        {
            _mainDbContext = mainDbContext;
            _currentUserService = currentUserService;

        }
        public async Task<ArticleDTO> Handle(DeleteArticleFavouriteCommand request, CancellationToken cancellationToken)
        {
            var article = await _mainDbContext.Articles.Include(x => x.Author).Where(x => x.Slug == request.Slug && x.AuthorId == _currentUserService.Id).FirstOrDefaultAsync(cancellationToken);
            if (article.Slug == null)
            {
                throw new RestException(HttpStatusCode.NotFound,$"{request.Slug} is not exits");
            }
            var deleteFavourite = await _mainDbContext.Favourites.Where(x=> x.ArticleId == article.Id).FirstOrDefaultAsync(cancellationToken);
            if (deleteFavourite == null)
            {
                throw new RestException(HttpStatusCode.NotFound,$"{request.Slug} has not been favourite");
            }
            _mainDbContext.Favourites.Remove(deleteFavourite);
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
                Favorited = false,
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

