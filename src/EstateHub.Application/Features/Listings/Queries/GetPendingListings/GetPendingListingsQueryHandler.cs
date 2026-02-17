using EstateHub.Application.Common.Models;
using EstateHub.Application.Interfaces;
using EstateHub.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using EstateHub.Domain.Entities;

namespace EstateHub.Application.Features.Listings.Queries.GetPendingListings;

public class GetPendingListingsQueryHandler
    : IRequestHandler<GetPendingListingsQuery, Result<PaginatedList<PendingListingDto>>>
{
    private readonly IRepositoryManager _manager;
    private readonly UserManager<AppUser> _userManager;

    public GetPendingListingsQueryHandler(
        IRepositoryManager manager,
        UserManager<AppUser> userManager)
    {
        _manager = manager;
        _userManager = userManager;
    }

    public async Task<Result<PaginatedList<PendingListingDto>>> Handle(
        GetPendingListingsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _manager.Listings
            .GetQueryable()
            .Where(x => x.Status == ListingStatus.Pending)
            .OrderBy(x => x.CreatedAt);

        var projectedQuery = query.Select(x => new PendingListingDto
        {
            Id = x.Id,
            Title = x.Title,
            Price = x.Price,
            Currency = x.Currency,
            PropertyType = x.PropertyType,
            ListingType = x.ListingType,
            City = x.Location!.City,
            CreatedAt = x.CreatedAt,
            OwnerFullName = string.Empty,
            OwnerEmail = string.Empty
        });

        var result = await PaginatedList<PendingListingDto>
            .CreateAsync(projectedQuery, request.PageNumber, request.PageSize);

        // Owner məlumatlarını əlavə et
        foreach (var item in result.Items)
        {
            var listing = await _manager.Listings
                .GetByIdAsync(item.Id, asNoTracking: true);

            if (listing != null)
            {
                var owner = await _userManager.FindByIdAsync(listing.OwnerId.ToString());
                if (owner != null)
                {
                    item.OwnerFullName = $"{owner.FirstName} {owner.LastName}";
                    item.OwnerEmail = owner.Email!;
                }
            }
        }

        return Result<PaginatedList<PendingListingDto>>.Success(result);
    }
}