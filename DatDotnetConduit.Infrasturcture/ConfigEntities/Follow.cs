using DatDotnetConduit.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DatDotnetConduit.Infrasturcture.ConfigEntities
{
    internal class FollowEntityConfiguration : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.ToTable(nameof(Follow), MainDbContext.UserSchema);
            builder.HasOne(x => x.Following).WithMany(x => x.Followings).HasForeignKey(x => x.FollowingId);
            builder.HasOne(x => x.Follower).WithMany(x => x.Followers).HasForeignKey(x => x.FollowerId);
        }
    }
}
