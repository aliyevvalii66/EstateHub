using EstateHub.Application.Common.Models;
using MediatR;

namespace EstateHub.Application.Features.Listings.Commands.RejectListing;

public record RejectListingCommand : IRequest<Result>
{
    public Guid ListingId { get; init; }
    public Guid AdminId { get; init; }
    public string Reason { get; init; } = string.Empty;
}