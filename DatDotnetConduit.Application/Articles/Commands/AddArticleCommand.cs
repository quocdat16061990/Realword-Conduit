using DatDotnetConduit.Application.Articles.Common;
using DatDotnetConduit.Application.Common;
using DatDotnetConduit.Application.Users.Common;
using DatDotnetConduit.Domain.Entities;
using DatDotnetConduit.Infrasturcture;
using DatDotnetConduit.Infrasturcture.Auth;
using DatDotnetConduit.Infrasturcture.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DatDotnetConduit.Application.Articles.Commands
{
    public  class AddArticleCommand : IRequest<ArticleDTO>
    {
        public string Title { get; init; }
        public string ContentArticle { get; init; }
        public string Description { get; init; }
        public List<string> Tags { get; init; }

    }
    internal class AddArticleCommandHandler : IRequestHandler<AddArticleCommand, ArticleDTO>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ICurrentUser _currentUserService;
       
        public AddArticleCommandHandler (MainDbContext mainDbContext, ICurrentUser currentUserService )
        {
            _mainDbContext = mainDbContext;
            _currentUserService = currentUserService;
           
        }
        public async Task<ArticleDTO> Handle (AddArticleCommand request ,CancellationToken cancellationToken)
        {
            if(!CheckUnique(request.Tags))
            {
                throw new RestException(HttpStatusCode.NotFound," Tags is dupplicated ");
            }
            var newArticle = new Article
            {   
               
                Description = request.Description,
                Title = request.Title,
                Content = request.ContentArticle,
               
                Tags = request.Tags,
                AuthorId = _currentUserService.Id.Value,
                Slug = CreateSlug(request.Title)

            };
            _mainDbContext.Articles.Add(newArticle);
            await _mainDbContext.SaveChangesAsync(cancellationToken);

            var author = await _mainDbContext.Users.AsNoTracking().Where(x=>x.Username == _currentUserService.Username)
                .Select(x=> new UserDto
                {
                    Username = x.Username,
                    Image = x.Image,
                    Description = x.Description,
                    Email = x.Email,
                })
                .FirstOrDefaultAsync(cancellationToken);
       
            return new ArticleDTO
            {
                Description = newArticle.Description,
                Title = newArticle.Title,
                ContentArticle = newArticle.Content,
                Tags = newArticle.Tags,
                Id = newArticle.Id,
                CreatedAt = newArticle.CreatedAt,
                UpdatedAt = newArticle.UpdatedAt,
                Favorited = false,
                FavoritesCount =0,
                Author = new UserDto
                {
                    Username =  author.Username,
                    Image=author.Image,
                    Description= author.Description,
                    Email= author.Email,

                },
                Slug = newArticle.Slug,

            };

        }
        private string CreateSlug (string title)
        {
            var slug = title.Replace(" ", "-");
            return slug;
        }

        private bool CheckUnique (List<string> tags)
        {
           for (int i = 1;i<tags.Count;i++)
            {
                for(int j = 0;j<i-1;j++)
                {
                    if (tags[i] == tags[j]) { return false; }
                }
            }
            return true;
        }
    }
}
