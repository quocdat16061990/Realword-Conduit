using DatDotnetConduit.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatDotnetConduit.Domain.Entities
{
    public class Article : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Content{ get; set; }

        public string Description { get; set; }
        public List<String> Tags { get; set; } 

        public string Slug { get; set; }
        public Guid AuthorId { get; set; }
        public User Author { get; set; }

        public ICollection <Favourite> Favorites { get; set; }

   


    }
}
