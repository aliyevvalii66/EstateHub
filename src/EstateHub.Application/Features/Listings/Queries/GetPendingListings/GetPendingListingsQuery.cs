using EstateHub.Application.Common.Models;
using MediatR;

namespace EstateHub.Application.Features.Listings.Queries.GetPendingListings;

public record GetPendingListingsQuery : IRequest<Result<PaginatedList<PendingListingDto>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}