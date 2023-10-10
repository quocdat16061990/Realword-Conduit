using DatDotnetConduit.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatDotnetConduit.Infrasturcture.ConfigEntities
{
    internal class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable(nameof(Comment), MainDbContext.ArticleSchema);
           
            builder.HasIndex(x => x.AuthorId);
            builder.HasIndex(x => x.ArticleId);
        }
    }
}
