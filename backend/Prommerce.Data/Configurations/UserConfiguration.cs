using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prommerce.Data.Entites;

namespace Prommerce.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasIndex(u => u.Identifier)
                .IsUnique();

            builder
                .Property(u => u.Identifier)
                .HasMaxLength(30);

            builder
                .Property(u => u.FirstName)
                .HasMaxLength(100);

            builder
                .Property(u => u.LastName)
                .HasMaxLength(50);

            builder
                .Property(u => u.Email)
                .HasMaxLength(170);

            builder
                .Property(u => u.PhoneNumber)
                .HasMaxLength(20);
        }
    }
}