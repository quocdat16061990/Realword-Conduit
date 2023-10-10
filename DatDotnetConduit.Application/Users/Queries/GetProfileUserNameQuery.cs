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

namespace DatDotnetConduit.Application.Users.Queries
{
    public class GetProfileUserNameQuery : IRequest<UserDto>
    {
        public String UserName { get; init; }
    }
    internal class GetProfileUserNameHandler : IRequestHandler<GetProfileUserNameQuery, UserDto>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ICurrentUser _currentUser;
        public GetProfileUserNameHandler(MainDbContext mainDbContext, ICurrentUser currentUser)
        {
            _mainDbContext = mainDbContext;
            _currentUser = currentUser;
        }

        public async Task<UserDto> Handle(GetProfileUserNameQuery request, CancellationToken cancellationToken)
        {
            var userProfile = await _mainDbContext.Users.AsNoTracking().Where(a => a.Username == request.UserName).Select(a => new UserDto
            {
                Description = a.Description,
                Email = a.Email,
                Image = a.Image,
                Username = a.Username,
            }).FirstOrDefaultAsync(cancellationToken);
            if (userProfile == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "User Profile does not exits");
            }

            return userProfile;
        }

    }
}
