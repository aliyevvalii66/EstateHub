using EstateHub.Application.Common.Models;
using MediatR;

namespace EstateHub.Application.Features.Listings.Queries.GetListingById;

public record GetListingByIdQuery(Guid Id) : IRequest<Result<ListingDetailDto>>;