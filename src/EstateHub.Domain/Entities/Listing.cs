using EstateHub.Domain.Common;
using EstateHub.Domain.Enums;

namespace EstateHub.Domain.Entities;

public class Listing : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = "AZN";

    public PropertyType PropertyType { get; set; }
    public ListingType ListingType { get; set; }
    // yalnız Rent üçün
    public RentPeriod? RentPeriod { get; set; }  
    public ListingStatus Status { get; set; } = ListingStatus.Pending;

    public Guid OwnerId { get; set; }

    // Navigation properties
    public ListingDetail? Detail { get; set; }
    public Location? Location { get; set; }
    public ICollection<ListingImage> Images { get; set; } = new List<ListingImage>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    public ICollection<AdminReviewLog> AdminReviewLogs { get; set; } = new List<AdminReviewLog>();
}