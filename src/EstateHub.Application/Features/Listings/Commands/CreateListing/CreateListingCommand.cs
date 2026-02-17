using EstateHub.Application.Common.Models;
using EstateHub.Domain.Enums;
using MediatR;

namespace EstateHub.Application.Features.Listings.Commands.CreateListing;

public record CreateListingCommand : IRequest<Result<Guid>>
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public string Currency { get; init; } = "AZN";
    public PropertyType PropertyType { get; init; }
    public ListingType ListingType { get; init; }
    public RentPeriod? RentPeriod { get; init; }

    // Detail
    public double Area { get; init; }
    public int? Rooms { get; init; }
    public int? Floor { get; init; }
    public int? TotalFloors { get; init; }
    public bool HasGarage { get; init; }
    public bool HasGarden { get; init; }
    public bool HasPool { get; init; }
    public int? BuildYear { get; init; }

    // Location
    public string City { get; init; } = string.Empty;
    public string District { get; init; } = string.Empty;
    public string? Address { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }

    public Guid OwnerId { get; init; }
}