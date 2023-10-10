using DatDotnetConduit.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatDotnetConduit.Domain.Entities
{
   
    public class Favourite : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public Guid ArticleId { get; set; }
        public User User { get; set; }
        public Article Article { get; set; }

    }
}


