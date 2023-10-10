using DatDotnetConduit.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatDotnetConduit.Domain.Entities
{
    public class Comment : BaseEntity<Guid>
    {
        public string CommentContent { get; set; }
        public Guid ArticleId { get; set; }

        public Guid AuthorId { get; set; }
        public User Author { get; set; }
        public Article Article { get; set; }
    }
}