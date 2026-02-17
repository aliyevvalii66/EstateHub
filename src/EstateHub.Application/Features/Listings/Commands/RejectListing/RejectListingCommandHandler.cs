using EstateHub.Application.Common.Exceptions;
using EstateHub.Application.Common.Models;
using EstateHub.Application.Interfaces;
using EstateHub.Domain.Entities;
using EstateHub.Domain.Enums;
using MediatR;

namespace EstateHub.Application.Features.Listings.Commands.RejectListing;

public class RejectListingCommandHandler : IRequestHandler<RejectListingCommand, Result>
{
    private readonly IRepositoryManager _manager;

    public RejectListingCommandHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Result> Handle(
        RejectListingCommand request,
        CancellationToken cancellationToken)
    {
        await _manager.BeginTransactionAsync();
        try
        {
            var listing = await _manager.Listings
                .GetByIdAsync(request.ListingId, asNoTracking: false);

            if (listing == null)
                throw new NotFoundException("Listing", request.ListingId);

            if (listing.Status != ListingStatus.Pending)
                return Result.Failure("Yalnız gözləmədə olan elanlar rədd edilə bilər.");

            listing.Status = ListingStatus.Rejected;
            listing.UpdatedAt = DateTime.UtcNow;
            _manager.Listings.Update(listing);

            var log = new AdminReviewLog
            {
                Id = Guid.NewGuid(),
                ListingId = request.ListingId,
                AdminId = request.AdminId,
                Action = "Rejected",
                Reason = request.Reason,
                ReviewedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };
            await _manager.AdminReviewLogs.AddAsync(log);

            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                UserId = listing.OwnerId,
                Title = "Elanınız rədd edildi",
                Message = $"'{listing.Title}' elanınız rədd edildi. Səbəb: {request.Reason}",
                Type = "ListingRejected",
                RelatedEntityId = listing.Id.ToString(),
                CreatedAt = DateTime.UtcNow
            };
            await _manager.Notifications.AddAsync(notification);

            await _manager.SaveChangesAsync();
            await _manager.CommitTransactionAsync();

            return Result.Success();
        }
        catch (NotFoundException)
        {
            await _manager.RollbackTransactionAsync();
            throw;
        }
        catch
        {
            await _manager.RollbackTransactionAsync();
            return Result.Failure("Elan rədd edilərkən xəta baş verdi.");
        }
    }
}