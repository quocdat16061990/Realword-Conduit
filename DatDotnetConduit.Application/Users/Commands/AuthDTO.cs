using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatDotnetConduit.Application.Users.Commands
{
    public class AuthDTO
    {
        public string UserName { get; init; }
        public string Email { get; init; }   
        public string Image { get; init; }
        public string Description { get; init; }
        public string AccessToken { get; init; }
    }
}
