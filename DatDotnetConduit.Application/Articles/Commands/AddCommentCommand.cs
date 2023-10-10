using DatDotnetConduit.Application.Articles.Common;
using DatDotnetConduit.Application.Users.Common;
using DatDotnetConduit.Domain.Entities;
using DatDotnetConduit.Infrasturcture.Auth;
using DatDotnetConduit.Infrasturcture;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DatDotnetConduit.Infrasturcture.Exceptions;
using System.Net;

namespace DatDotnetConduit.Application.Articles.Commands
{
    public class AddCommentRequest : IRequest<CommentDTO>
    {
       public string Body { get; init; }

    }
    public class AddCommentCommand : IRequest<CommentDTO>
    {
        public string Slug { get; init; }
        public AddCommentRequest Comment { get; init; }
    }
    internal class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, CommentDTO>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ICurrentUser _currentUserService;
 
        public AddCommentCommandHandler(MainDbContext mainDbContext, ICurrentUser currentUserService, IAuthService authService)
        {
            _mainDbContext = mainDbContext;
            _currentUserService = currentUserService;
          
        }
        public async Task<CommentDTO> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var author = await _mainDbContext.Users.AsNoTracking().Where(x => x.Username == _currentUserService.Username).FirstOrDefaultAsync(cancellationToken);
            var article = await _mainDbContext.Articles.AsNoTracking().Where(x=> x.Slug == request.Slug).FirstOrDefaultAsync(cancellationToken);
            if (article == null)
            {
                throw new RestException(HttpStatusCode.NotFound,$"Article with Slug '{request.Slug}' not found");
            }
            var newComment = new Comment
            {
                CommentContent = request.Comment.Body,
                
                ArticleId = article.Id,
                AuthorId = author.Id,
            };
            _mainDbContext.Comments.Add(newComment);
            await _mainDbContext.SaveChangesAsync(cancellationToken);

            return new CommentDTO
            {
                Body = newComment.CommentContent,
                Id = newComment.Id,
                Author = new UserDto
                {
                    Username = author.Username,
                    Image = author.Image,
                    Description = author.Description,
                    Email = author.Email
                }
            };

        }

    }
}
