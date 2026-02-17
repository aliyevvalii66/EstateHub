using EstateHub.Application.Common.Exceptions;
using EstateHub.Application.Common.Models;
using EstateHub.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using EstateHub.Domain.Entities;

namespace EstateHub.Application.Features.Listings.Queries.GetListingById;

public class GetListingByIdQueryHandler
    : IRequestHandler<GetListingByIdQuery, Result<ListingDetailDto>>
{
    private readonly IRepositoryManager _manager;
    private readonly UserManager<AppUser> _userManager;

    public GetListingByIdQueryHandler(
        IRepositoryManager manager,
        UserManager<AppUser> userManager)
    {
        _manager = manager;
        _userManager = userManager;
    }

    public async Task<Result<ListingDetailDto>> Handle(
        GetListingByIdQuery request,
        CancellationToken cancellationToken)
    {
        var listing = await _manager.Listings.GetListingWithDetailsAsync(request.Id);

        if (listing == null)
            throw new NotFoundException("Listing", request.Id);

        var owner = await _userManager.FindByIdAsync(listing.OwnerId.ToString());
        var averageRating = await _manager.Reviews.GetAverageRatingAsync(listing.Id);

        var dto = new ListingDetailDto
        {
            Id = listing.Id,
            Title = listing.Title,
            Description = listing.Description,
            Price = listing.Price,
            Currency = listing.Currency,
            PropertyType = listing.PropertyType,
            ListingType = listing.ListingType,
            RentPeriod = listing.RentPeriod,
            Status = listing.Status,
            CreatedAt = listing.CreatedAt,
            OwnerId = listing.OwnerId,
            OwnerFullName = owner != null
                ? $"{owner.FirstName} {owner.LastName}"
                : "Naməlum",
            OwnerPhone = owner?.PhoneNumber,
            AverageRating = averageRating,
            ReviewCount = listing.Reviews.Count,

            // Detail
            Area = listing.Detail?.Area ?? 0,
            Rooms = listing.Detail?.Rooms,
            Floor = listing.Detail?.Floor,
            TotalFloors = listing.Detail?.TotalFloors,
            HasGarage = listing.Detail?.HasGarage ?? false,
            HasGarden = listing.Detail?.HasGarden ?? false,
            HasPool = listing.Detail?.HasPool ?? false,
            BuildYear = listing.Detail?.BuildYear,

            // Location
            City = listing.Location?.City ?? string.Empty,
            District = listing.Location?.District ?? string.Empty,
            Address = listing.Location?.Address,
            Latitude = listing.Location?.Latitude,
            Longitude = listing.Location?.Longitude,

            // Images
            Images = listing.Images
                .OrderBy(x => x.Order)
                .Select(x => new ListingImageDto
                {
                    Url = x.Url,
                    IsCover = x.IsCover,
                    Order = x.Order
                }).ToList()
        };

        return Result<ListingDetailDto>.Success(dto);
    }
}