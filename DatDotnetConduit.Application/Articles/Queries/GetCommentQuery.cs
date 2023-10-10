using DatDotnetConduit.Application.Users.Common;
using DatDotnetConduit.Infrasturcture.Auth;
using DatDotnetConduit.Infrasturcture;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatDotnetConduit.Application.Articles.Common;
using Microsoft.EntityFrameworkCore;
using DatDotnetConduit.Infrasturcture.Exceptions;
using System.Net;

namespace DatDotnetConduit.Application.Articles.Queries
{
    public class GetCommentQuery : IRequest<CommentDTO>
    {
        public string Slug { get; init; }
    }
    internal class GetCommentQueryHandler : IRequestHandler<GetCommentQuery, CommentDTO>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ICurrentUser _currentUser;
        public GetCommentQueryHandler(MainDbContext mainDbContext, ICurrentUser currentUser)
        {
            _mainDbContext = mainDbContext;
            _currentUser = currentUser;
        }

        public async Task<CommentDTO> Handle(GetCommentQuery request, CancellationToken cancellationToken)
        {

            var comment = await _mainDbContext.Comments
            .Include(x => x.Author)
            .Where(c => c.Article.Slug == request.Slug)
            .FirstOrDefaultAsync(cancellationToken);

            if (comment == null)
            {
                throw new RestException(HttpStatusCode.NotFound,"Comment does not exits");
            }


            return new CommentDTO
            {
                Body = comment.CommentContent,
                Author = new UserDto
                {
                    Username = comment.Author.Username,
                    Email = comment.Author.Email,
                    Image = comment.Author.Image,
                    Description = comment.Author.Description
                   
                }
            };
        }
    }
   }
    

