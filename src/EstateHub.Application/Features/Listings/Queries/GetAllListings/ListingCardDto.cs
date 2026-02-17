using EstateHub.Domain.Enums;

namespace EstateHub.Application.Features.Listings.Queries.GetAllListings;

public class ListingCardDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = string.Empty;
    public PropertyType PropertyType { get; set; }
    public ListingType ListingType { get; set; }
    public RentPeriod? RentPeriod { get; set; }
    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public double Area { get; set; }
    public int? Rooms { get; set; }
    public string? CoverImageUrl { get; set; }
    public double AverageRating { get; set; }
    public DateTime CreatedAt { get; set; }
}