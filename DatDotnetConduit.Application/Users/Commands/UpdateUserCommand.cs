using DatDotnetConduit.Application.Users.Common;
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

namespace DatDotnetConduit.Application.Users.Commands
{
    public class UpdateUserCommand : IRequest<UserDto>
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string Description { get; init; }

    
        public string Image { get; init; }
    }
    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly MainDbContext _mainDbContext;

        private ICurrentUser _currentUser;
        public UpdateUserCommandHandler(MainDbContext mainDbContext, ICurrentUser currentUser)
        {
            _mainDbContext = mainDbContext;

            _currentUser = currentUser;
        }
        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userCurrent = await _mainDbContext.Users.FirstOrDefaultAsync(a => a.Id == _currentUser.Id, cancellationToken);
            if (userCurrent is null)
            {
                throw new RestException(HttpStatusCode.NotFound,"UserName dose not exits");
            }
            userCurrent.Username = request.Username;
            userCurrent.Email = request.Email;
            userCurrent.Description = request.Description;
            userCurrent.Image = request.Image;
            await _mainDbContext.SaveChangesAsync(cancellationToken);
            return new UserDto { Username = userCurrent.Username, Email = userCurrent.Email, Description = userCurrent.Description, IsFollowed = false, Image = userCurrent.Image };

        }
    }
}