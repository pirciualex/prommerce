using Microsoft.EntityFrameworkCore;
using Prommerce.Data.Entites;

namespace Prommerce.Data
{
    public class Db : DbContext
    {
        public Db(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var when = DateTimeOffset.UtcNow;
            var who = Guid.Parse("de0297d9-33d9-4abb-b9d2-9eb36dccb01f");

            // TODO: uncomment after implementing authentication
            // var who = _httpContext?.User.GetIdentifier();

            foreach (var entry in ChangeTracker.Entries())
            {
                var type = entry.Entity.GetType();

                if (typeof(BaseEntity).IsAssignableFrom(type.BaseType) && entry.State == EntityState.Added && (entry.Entity as BaseEntity).Id == Guid.Empty)
                {
                    (entry.Entity as BaseEntity).Id = Guid.NewGuid();
                }

                if (typeof(ISoftDeleteEntity).IsAssignableFrom(type.BaseType) && entry.State == EntityState.Deleted)
                {
                    (entry.Entity as ISoftDeleteEntity).Deleted = true;
                    entry.State = EntityState.Modified;
                }

                if (typeof(ITrackedEntity).IsAssignableFrom(type.BaseType))
                {
                    var trackedEntity = entry.Entity as ITrackedEntity;

                    if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                    {
                        trackedEntity.ModifiedDate = when;
                        trackedEntity.ModifiedBy = who.ToString();
                    }
                    if (entry.State == EntityState.Added)
                    {
                        trackedEntity.CreatedDate = when;
                        trackedEntity.CreatedBy = who.ToString();
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}