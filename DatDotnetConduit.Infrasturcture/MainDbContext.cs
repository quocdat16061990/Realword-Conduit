using DatDotnetConduit.Domain.Common;
using DatDotnetConduit.Domain.Entities;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DatDotnetConduit.Infrasturcture
{
    public class MainDbContext : DbContext
    {
        public const string UserSchema = "User";
        public const string ArticleSchema = "Article";
        
        public DbSet<User> Users { get; set; }
        public DbSet<Follow> Follows { get; set; }

        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Article> Articles { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            AddAuditInfo();
            return await base.SaveChangesAsync(cancellationToken);
        }
        public override int SaveChanges()
        {
            AddAuditInfo();
            return base.SaveChanges();
        }

        private void AddAuditInfo()
        {
            var entries = ChangeTracker.Entries()
                 .Where(a => a.Entity is IAuditEntity && (a.State == EntityState.Added || a.State == EntityState.Modified));
            foreach (var entityEntry in entries)
            {
                var entity = (IAuditEntity)entityEntry.Entity;
                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }
                else
                {
                    Entry(entity).Property(p => p.CreatedAt).IsModified = false;
                }
                entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
