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
    public class UnfollowingCommand : IRequest<UserDto>
    {
        public string FollowerUserName { get; init; }

    }
    internal class UnfollowingCommandHandler : IRequestHandler<UnfollowingCommand, UserDto>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ICurrentUser _currentUser;
        public UnfollowingCommandHandler(MainDbContext mainDbContext, ICurrentUser currentUser)
        {
            _mainDbContext = mainDbContext;
            _currentUser = currentUser;

        }
        public async Task<UserDto> Handle(UnfollowingCommand request, CancellationToken cancellationToken)
        {
            var unfollowerUser = await _mainDbContext.Users.Where(a => a.Username == request.FollowerUserName).FirstOrDefaultAsync(cancellationToken);
            if (unfollowerUser == null)
            {
                throw new RestException(HttpStatusCode.NotFound,$"{request.FollowerUserName} is not exits");
            }
            var follow = await _mainDbContext.Follows.FirstOrDefaultAsync(a => a.FollowerId == unfollowerUser.Id && a.FollowingId == _currentUser.Id.Value);

            if(follow == null)
            {
                throw new RestException(HttpStatusCode.NotFound,$"{request.FollowerUserName} has not been followed");
            }
                  _mainDbContext.Follows.Remove(follow);
                await _mainDbContext.SaveChangesAsync(cancellationToken);
            
            return new UserDto
            {
                Username = unfollowerUser.Username,
                Email = unfollowerUser.Email,
                Description = unfollowerUser.Description,
                IsFollowed = false,
                Image = unfollowerUser.Image,
            };

        }
    }
}
