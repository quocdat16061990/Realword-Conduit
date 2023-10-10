using DatDotnetConduit.Infrasturcture.Auth;
using DatDotnetConduit.Infrasturcture;
using MediatR;
using DatDotnetConduit.Infrasturcture.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DatDotnetConduit.Application.Users.Commands
{
    public class LoginUserCommand : IRequest<AuthDTO>
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
    internal class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthDTO>
    {
        private readonly MainDbContext _mainDbContext;
        private IAuthService _authService;
        public LoginUserCommandHandler(MainDbContext mainDbContext, IAuthService authService)
        {
            _mainDbContext = mainDbContext;
            _authService = authService;
        }
        public async Task<AuthDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _mainDbContext.Users.FirstOrDefaultAsync(a => a.Username == request.Username);
            if (user is null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Username does not exits");
            }
            bool checkPassword = _authService.VerifyPassword(user.Password, request.Password);
            if (!checkPassword)
            {
                throw new RestException(HttpStatusCode.Unauthorized, "Password incorrect");
            }

            return new AuthDTO { UserName = user.Username, Email = user.Email, Description = user.Description, Image = user.Image, AccessToken = _authService.GenerateToken(user) };



        }
    }
}