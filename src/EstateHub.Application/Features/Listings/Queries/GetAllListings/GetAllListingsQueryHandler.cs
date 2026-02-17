using EstateHub.Application.Common.Models;
using EstateHub.Application.Interfaces;
using EstateHub.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EstateHub.Application.Features.Listings.Queries.GetAllListings;

public class GetAllListingsQueryHandler
    : IRequestHandler<GetAllListingsQuery, Result<PaginatedList<ListingCardDto>>>
{
    private readonly IRepositoryManager _manager;

    public GetAllListingsQueryHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Result<PaginatedList<ListingCardDto>>> Handle(
        GetAllListingsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _manager.Listings
            .GetQueryable()
            .Where(x => x.Status == ListingStatus.Approved);

        // Filterlər
        if (!string.IsNullOrEmpty(request.City))
            query = query.Where(x => x.Location!.City == request.City);

        if (!string.IsNullOrEmpty(request.District))
            query = query.Where(x => x.Location!.District == request.District);

        if (request.PropertyType.HasValue)
            query = query.Where(x => x.PropertyType == request.PropertyType);

        if (request.ListingType.HasValue)
            query = query.Where(x => x.ListingType == request.ListingType);

        if (request.RentPeriod.HasValue)
            query = query.Where(x => x.RentPeriod == request.RentPeriod);

        if (request.MinPrice.HasValue)
            query = query.Where(x => x.Price >= request.MinPrice);

        if (request.MaxPrice.HasValue)
            query = query.Where(x => x.Price <= request.MaxPrice);

        if (request.MinArea.HasValue)
            query = query.Where(x => x.Detail!.Area >= request.MinArea);

        if (request.MaxArea.HasValue)
            query = query.Where(x => x.Detail!.Area <= request.MaxArea);

        if (request.Rooms.HasValue)
            query = query.Where(x => x.Detail!.Rooms == request.Rooms);

        if (request.HasGarage.HasValue)
            query = query.Where(x => x.Detail!.HasGarage == request.HasGarage);

        if (request.HasPool.HasValue)
            query = query.Where(x => x.Detail!.HasPool == request.HasPool);

        // Sıralama
        query = request.SortBy switch
        {
            "price" => request.SortDescending
                ? query.OrderByDescending(x => x.Price)
                : query.OrderBy(x => x.Price),
            "area" => request.SortDescending
                ? query.OrderByDescending(x => x.Detail!.Area)
                : query.OrderBy(x => x.Detail!.Area),
            _ => query.OrderByDescending(x => x.CreatedAt)
        };

        // Projection — yalnız lazımlı sahələri seç
        var projectedQuery = query.Select(x => new ListingCardDto
        {
            Id = x.Id,
            Title = x.Title,
            Price = x.Price,
            Currency = x.Currency,
            PropertyType = x.PropertyType,
            ListingType = x.ListingType,
            RentPeriod = x.RentPeriod,
            City = x.Location!.City,
            District = x.Location.District,
            Area = x.Detail!.Area,
            Rooms = x.Detail.Rooms,
            CoverImageUrl = x.Images
                .Where(i => i.IsCover)
                .Select(i => i.Url)
                .FirstOrDefault(),
            CreatedAt = x.CreatedAt
        });

        var result = await PaginatedList<ListingCardDto>
            .CreateAsync(projectedQuery, request.PageNumber, request.PageSize);

        return Result<PaginatedList<ListingCardDto>>.Success(result);
    }
}