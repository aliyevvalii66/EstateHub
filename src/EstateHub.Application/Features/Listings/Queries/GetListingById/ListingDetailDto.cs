using EstateHub.Domain.Enums;

namespace EstateHub.Application.Features.Listings.Queries.GetListingById;

public class ListingDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = string.Empty;
    public PropertyType PropertyType { get; set; }
    public ListingType ListingType { get; set; }
    public RentPeriod? RentPeriod { get; set; }
    public ListingStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public double Area { get; set; }
    public int? Rooms { get; set; }
    public int? Floor { get; set; }
    public int? TotalFloors { get; set; }
    public bool HasGarage { get; set; }
    public bool HasGarden { get; set; }
    public bool HasPool { get; set; }
    public int? BuildYear { get; set; }

    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string? Address { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public Guid OwnerId { get; set; }
    public string OwnerFullName { get; set; } = string.Empty;
    public string? OwnerPhone { get; set; }

    public List<ListingImageDto> Images { get; set; } = new();

    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
}

public class ListingImageDto
{
    public string Url { get; set; } = string.Empty;
    public bool IsCover { get; set; }
    public int Order { get; set; }
}