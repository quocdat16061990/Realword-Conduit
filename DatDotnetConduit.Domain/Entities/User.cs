using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatDotnetConduit.Domain.Common;

namespace DatDotnetConduit.Domain.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
        public string Image { get;set; }
        public string Description { get; set; }
        public ICollection<Follow> Followers { get; set; }
        public ICollection<Follow> Followings { get; set; }
        public ICollection<Favourite> Favourites { get; set; }

        public ICollection<Article> Articles { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
