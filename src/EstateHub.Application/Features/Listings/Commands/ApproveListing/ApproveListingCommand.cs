using EstateHub.Application.Common.Models;
using MediatR;

namespace EstateHub.Application.Features.Listings.Commands.ApproveListing;

public record ApproveListingCommand : IRequest<Result>
{
    public Guid ListingId { get; init; }
    public Guid AdminId { get; init; }
}