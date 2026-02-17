using EstateHub.Application.Common.Models;
using EstateHub.Domain.Enums;
using MediatR;

namespace EstateHub.Application.Features.Listings.Queries.GetAllListings;

public record GetAllListingsQuery : IRequest<Result<PaginatedList<ListingCardDto>>>
{
    // Filterlər
    public string? City { get; init; }
    public string? District { get; init; }
    public PropertyType? PropertyType { get; init; }
    public ListingType? ListingType { get; init; }
    public RentPeriod? RentPeriod { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public double? MinArea { get; init; }
    public double? MaxArea { get; init; }
    public int? Rooms { get; init; }
    public bool? HasGarage { get; init; }
    public bool? HasPool { get; init; }

    // Sıralama
    public string? SortBy { get; init; }        // price, date, area
    public bool SortDescending { get; init; } = true;

    // Pagination
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 12;
}