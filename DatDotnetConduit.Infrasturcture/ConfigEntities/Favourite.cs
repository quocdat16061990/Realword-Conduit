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
    internal class FavouriteEntityConfiguration : IEntityTypeConfiguration<Favourite>
    {
        public void Configure(EntityTypeBuilder<Favourite> builder)
        {
         
            builder.ToTable(nameof(Favourite), MainDbContext.ArticleSchema);
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.ArticleId);
            builder.HasIndex(x=> new {x.UserId, x.ArticleId }).IsUnique();
        }
    }
}
