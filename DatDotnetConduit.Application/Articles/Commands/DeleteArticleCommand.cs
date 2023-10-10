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
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using DatDotnetConduit.Application.Articles.Common;
using DatDotnetConduit.Infrasturcture.Exceptions;
using System.Net;

namespace DatDotnetConduit.Application.Articles.Commands
{
    public class DeleteArticleCommand : IRequest<DeletetDTO>
    {
        public string Slug { get; init; }    

    }
 
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, DeletetDTO>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ICurrentUser _currentUserService;
        
        public DeleteArticleCommandHandler(MainDbContext mainDbContext, ICurrentUser currentUserService, IAuthService authService)
        {
            _mainDbContext = mainDbContext;
            _currentUserService = currentUserService;
         
        }
        public async Task<DeletetDTO> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
         
            var article = await _mainDbContext.Articles.Include(x => x.Author).Where(x => x.Slug == request.Slug && x.AuthorId == _currentUserService.Id).FirstOrDefaultAsync(cancellationToken);
            if (article == null)
            {
                throw new RestException(HttpStatusCode.NotFound,"Article does not exits !");
            }
            
            _mainDbContext.Articles.Remove(article);
            await _mainDbContext.SaveChangesAsync(cancellationToken);


            return new DeletetDTO { Content = "Delete Article Successfully" };
           
        }

     
    }
}
