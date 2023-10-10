using DatDotnetConduit.Application.Articles.Common;
using DatDotnetConduit.Application.Users.Common;
using DatDotnetConduit.Domain.Entities;
using DatDotnetConduit.Infrasturcture.Auth;
using DatDotnetConduit.Infrasturcture;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DatDotnetConduit.Infrasturcture.Exceptions;
using System.Net;

namespace DatDotnetConduit.Application.Articles.Commands
{

    public class UpdateArticleRequest
    {
        public string Title { get; init; }
        public string Content { get; init; }
        public string Description { get; init; }
      
        public List<string> Tags { get; init; }
    }
    
    public class UpdateArticleCommand : IRequest<ArticleDTO>
    {
       
        public string Slug { get; init; }
        public UpdateArticleRequest Body { get; init; }

    }
    internal class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, ArticleDTO>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ICurrentUser _currentUserService;
        
        public UpdateArticleCommandHandler(MainDbContext mainDbContext, ICurrentUser currentUserService)
        {
            _mainDbContext = mainDbContext;
            _currentUserService = currentUserService;
        
        }
        public async Task<ArticleDTO> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            
            var article = await _mainDbContext.Articles.Include(x=>x.Author).Where(x => x.Slug == request.Slug && x.AuthorId == _currentUserService.Id).FirstOrDefaultAsync(cancellationToken);
            //   inclue join 
            if (article == null)
            {
                throw new RestException(HttpStatusCode.NotFound,"Article does not exits");
            }
            article.Title = request.Body.Title;
            article.Description = request.Body.Description;
            article.Content = request.Body.Content;
            article.Slug = CreateSlug(request.Body.Title);
            article.Tags = request.Body.Tags;
            await _mainDbContext.SaveChangesAsync(cancellationToken);

            return new ArticleDTO { Tags = article.Tags , Title= article.Title, Description =article.Description ,Slug = article.Slug ,ContentArticle = article.Content , 
                Author = new UserDto { Description = article.Author.Description , Email = article.Author.Email , Image = article.Author.Image , IsFollowed = false , Username = article.Author.Username } };

        }
        private string CreateSlug(string title)
        {
            var slug = title.Replace(" ", "-");
            return slug;
        }
    }
}
