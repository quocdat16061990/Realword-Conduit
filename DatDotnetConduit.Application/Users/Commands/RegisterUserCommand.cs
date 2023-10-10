using System;
using BCrypt.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatDotnetConduit.Domain.Entities;
using DatDotnetConduit.Infrasturcture;
using DatDotnetConduit.Infrasturcture.Auth;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DatDotnetConduit.Infrasturcture.Exceptions;
using System.Net;

namespace DatDotnetConduit.Application.Users.Commands
{
    public class RegisterUserCommand : IRequest<AuthDTO>
    {
        public string UserName { get; init; }
        public string Password { get; init; }
        public string Email { get; init; }
        public string Image { get; init; }
        public string Description { get; init; }
    }

    internal class RegisterUserComamndHandler : IRequestHandler<RegisterUserCommand, AuthDTO>
    {
        private readonly MainDbContext _mainDbContext;
        private IAuthService _authService;
        public RegisterUserComamndHandler(MainDbContext mainDbContext, IAuthService authService)
        {
            _mainDbContext = mainDbContext;
            _authService = authService;
        }
        public async Task<AuthDTO> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _mainDbContext.Users.AnyAsync(x => x.Username == request.UserName);
            if (userExists)
            {
                throw new RestException(HttpStatusCode.Unauthorized, "Username has been taken");
            }
            var emailExits = await _mainDbContext.Users.AnyAsync(x=> x.Email == request.Email); 
            if (emailExits)
            {
                throw new RestException(HttpStatusCode.Unauthorized, "Email has been taken");
            }
            var newUser = new User
            {
                Username = request.UserName,
                Password = _authService.HashPassword(request.Password),
                Email = request.Email,
                Image = request.Image,
                Description = request.Description,
            };
            _mainDbContext.Add(newUser);
            await _mainDbContext.SaveChangesAsync(cancellationToken);
    
            return new AuthDTO { UserName = newUser.Username, Email = newUser.Email, Image = newUser.Image, Description = newUser.Description, AccessToken = _authService.GenerateToken(newUser) };
        }
    }
}
