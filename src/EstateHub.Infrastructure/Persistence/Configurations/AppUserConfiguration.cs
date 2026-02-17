using EstateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstateHub.Infrastructure.Persistence.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ProfileImageUrl)
            .HasMaxLength(500);

        // Relations
        builder.HasMany(x => x.Listings)
            .WithOne()
            .HasForeignKey(x => x.OwnerId);

        builder.HasMany(x => x.Bookings)
            .WithOne()
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Reviews)
            .WithOne()
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Favorites)
            .WithOne()
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Notifications)
            .WithOne()
            .HasForeignKey(x => x.UserId);
    }
}