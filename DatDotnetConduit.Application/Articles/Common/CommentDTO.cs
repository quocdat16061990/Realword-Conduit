using DatDotnetConduit.Application.Users.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatDotnetConduit.Application.Articles.Common
{
    public class CommentDTO
    {
        public Guid Id { get; init; }
        public DateTime Created { get; init; }
        public DateTime Updated { get; init; }

        public string Body { get; init; }
        public UserDto Author { get; init; }
    }
}
