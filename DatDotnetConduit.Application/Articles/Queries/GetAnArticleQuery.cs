using DatDotnetConduit.Application.Articles.Common;
using DatDotnetConduit.Application.Users.Common;
using DatDotnetConduit.Infrasturcture;
using DatDotnetConduit.Infrasturcture.Auth;
using DatDotnetConduit.Infrasturcture.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DatDotnetConduit.Application.Articles.Queries
{
    public class GetAnArticleQuery: IRequest<ArticleDTO>
    {
        public string Slug { get; init; }
    }
    public class GetAnArticleQueryHandler: IRequestHandler<GetAnArticleQuery, ArticleDTO>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ICurrentUser _currentUserService;
        public GetAnArticleQueryHandler (MainDbContext mainDbContext , ICurrentUser currentUserService)
        {
            _mainDbContext = mainDbContext;
            _currentUserService = currentUserService;
        }
        
        public async Task<ArticleDTO> Handle (GetAnArticleQuery request, CancellationToken cancellationToken)
        {
            var article = await _mainDbContext.Articles.Include(x=> x.Author).ThenInclude(x=>x.Followers).Where(x => x.Slug == request.Slug).FirstOrDefaultAsync(cancellationToken);
            if(article == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Article does not exits");
            }
           
              return new ArticleDTO
            {
                Tags = article.Tags,
                Title = article.Title,
                Description = article.Description,
                Slug = article.Slug,
                ContentArticle = article.Content,
                Author = new UserDto { Description = article.Author.Description, Email = article.Author.Email, Image = article.Author.Image, IsFollowed = article.Author.Followers.Any(x => x.FollowingId == _currentUserService.Id), Username = article.Author.Username }
            };
        }
    }
}
