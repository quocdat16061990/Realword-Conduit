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

namespace DatDotnetConduit.Application.Users.Commands
{
    public class FollowingCommand : IRequest<UserDto>
    {
        public string FollowerUserName { get; init; }

    }
    internal class FollowingCommandHandler : IRequestHandler<FollowingCommand, UserDto>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ICurrentUser _currentUser;
        public FollowingCommandHandler(MainDbContext mainDbContext, ICurrentUser currentUser)
        {
            _mainDbContext = mainDbContext;
            _currentUser = currentUser;

        }
        public async Task<UserDto> Handle(FollowingCommand request, CancellationToken cancellationToken)
        {
            var followerUser = await _mainDbContext.Users.Where(a => a.Username == request.FollowerUserName).FirstOrDefaultAsync(cancellationToken);
            if (followerUser == null)
            {
                throw new RestException(HttpStatusCode.NotFound, $"{request.FollowerUserName} is not exits");
            }
            var follow = new Follow
            {
                FollowingId = _currentUser.Id.Value,
                FollowerId = followerUser.Id,
            };
            _mainDbContext.Follows.Add(follow);
            await _mainDbContext.SaveChangesAsync(cancellationToken);
            return new UserDto { Username = followerUser.Username, Email = followerUser.Email, Description = followerUser.Description, IsFollowed = true, Image = followerUser.Image };

        }
    }
}