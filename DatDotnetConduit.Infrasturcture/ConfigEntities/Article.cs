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
    internal class ArticleEntityConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable(nameof(Article), MainDbContext.ArticleSchema);
            builder.HasIndex(x => x.AuthorId);
            builder.HasIndex(x => x.Title).IsUnique();
            builder.HasIndex(x => x.Slug).IsUnique();
        }
    }
}
