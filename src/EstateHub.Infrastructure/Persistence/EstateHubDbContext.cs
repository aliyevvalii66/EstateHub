using EstateHub.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EstateHub.Infrastructure.Persistence;

public class EstateHubDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public EstateHubDbContext(DbContextOptions<EstateHubDbContext> options) : base(options)
    {
    }

    public DbSet<Listing> Listings { get; set; }
    public DbSet<ListingDetail> ListingDetails { get; set; }
    public DbSet<ListingImage> ListingImages { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<AdminReviewLog> AdminReviewLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(EstateHubDbContext).Assembly);
    }
}