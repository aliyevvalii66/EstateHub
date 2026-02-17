using EstateHub.Application.Common.Models;
using EstateHub.Application.Interfaces;
using EstateHub.Domain.Entities;
using EstateHub.Domain.Enums;
using MediatR;

namespace EstateHub.Application.Features.Listings.Commands.CreateListing;

public class CreateListingCommandHandler : IRequestHandler<CreateListingCommand, Result<Guid>>
{
    private readonly IRepositoryManager _manager;

    public CreateListingCommandHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Result<Guid>> Handle(
        CreateListingCommand request,
        CancellationToken cancellationToken)
    {
        await _manager.BeginTransactionAsync();
        try
        {
            var listing = new Listing
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Currency = request.Currency,
                PropertyType = request.PropertyType,
                ListingType = request.ListingType,
                RentPeriod = request.RentPeriod,
                Status = ListingStatus.Pending,
                OwnerId = request.OwnerId,
                CreatedAt = DateTime.UtcNow,
                Detail = new ListingDetail
                {
                    Id = Guid.NewGuid(),
                    Area = request.Area,
                    Rooms = request.Rooms,
                    Floor = request.Floor,
                    TotalFloors = request.TotalFloors,
                    HasGarage = request.HasGarage,
                    HasGarden = request.HasGarden,
                    HasPool = request.HasPool,
                    BuildYear = request.BuildYear,
                    CreatedAt = DateTime.UtcNow
                },
                Location = new Location
                {
                    Id = Guid.NewGuid(),
                    City = request.City,
                    District = request.District,
                    Address = request.Address,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    CreatedAt = DateTime.UtcNow
                }
            };

            await _manager.Listings.AddAsync(listing);

            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                UserId = request.OwnerId,
                Title = "Elanınız göndərildi",
                Message = "Elanınız admin tərəfindən yoxlanılır. Təsdiqləndikdən sonra aktiv olacaq.",
                Type = "ListingPending",
                RelatedEntityId = listing.Id.ToString(),
                CreatedAt = DateTime.UtcNow
            };

            await _manager.Notifications.AddAsync(notification);
            await _manager.SaveChangesAsync();
            await _manager.CommitTransactionAsync();

            return Result<Guid>.Success(listing.Id);
        }
        catch
        {
            await _manager.RollbackTransactionAsync();
            return Result<Guid>.Failure("Elan yaradılarkən xəta baş verdi.");
        }
    }
}