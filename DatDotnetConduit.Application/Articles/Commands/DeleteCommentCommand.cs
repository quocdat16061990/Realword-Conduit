using DatDotnetConduit.Application.Articles.Common;
using DatDotnetConduit.Domain.Entities;
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

namespace DatDotnetConduit.Application.Articles.Commands
{
    public class DeleteCommentCommand : IRequest<DeletetDTO>
    {
        public  Guid Id { get; init; }
        public string Slug { get; init; }
    }
   

    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, DeletetDTO>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ICurrentUser _currentUserService;

        public DeleteCommentCommandHandler(MainDbContext mainDbContext, ICurrentUser currentUserService)
        {
            _mainDbContext = mainDbContext;
            _currentUserService = currentUserService;
        }

        public async Task<DeletetDTO> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _mainDbContext.Comments
             .Where(c => c.Article.Slug == request.Slug && c.Id == request.Id)
             .FirstOrDefaultAsync(cancellationToken);
            if (comment == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Comment does not exits"); 
            }

            _mainDbContext.Comments.Remove(comment);
            await _mainDbContext.SaveChangesAsync(cancellationToken);
            return new DeletetDTO { Content = "Delete Comment Successfully" };
        }
    }
}
