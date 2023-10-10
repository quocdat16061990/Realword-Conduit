using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatDotnetConduit.Application.Users.Common
{
    public class UserDto
    {
        public string Username { get; init; }
        public string Email { get; init; }
        public string Image { get; init; }
        public string Description { get; init; }

        public bool IsFollowed { get; init; }
    }
}