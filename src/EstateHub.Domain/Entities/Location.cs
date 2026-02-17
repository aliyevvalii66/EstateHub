using EstateHub.Domain.Common;

namespace EstateHub.Domain.Entities;

public class Location : BaseEntity
{
    public Guid ListingId { get; set; }
    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string? Address { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    // Navigation
    public Listing Listing { get; set; } = null!;
}