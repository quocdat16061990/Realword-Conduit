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
    public class GetCurrentUserQuery : IRequest<UserDto>
    {
    }
    internal class CurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDto>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ICurrentUser _currentUser;
        public CurrentUserQueryHandler(MainDbContext mainDbContext, ICurrentUser currentUser)
        {
            _mainDbContext = mainDbContext;
            _currentUser = currentUser;
        }

        public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _mainDbContext.Users.AsNoTracking().Where(a => a.Id == _currentUser.Id).Select(a => new UserDto
            {
                Description = a.Description,
                Email = a.Email,
                Image = a.Image,
                Username = a.Username,

            }).FirstOrDefaultAsync(cancellationToken);
            if (user is null)
            {
                throw new RestException(HttpStatusCode.NotFound,$"User does not exits");
            }
            return user;
        }
    }
}