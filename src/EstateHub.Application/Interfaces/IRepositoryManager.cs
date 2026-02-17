using EstateHub.Application.Interfaces.Repositories;

namespace EstateHub.Application.Interfaces;

public interface IRepositoryManager
{
    IListingRepository Listings { get; }
    IBookingRepository Bookings { get; }
    IUserRepository Users { get; }
    IReviewRepository Reviews { get; }
    INotificationRepository Notifications { get; }
    IFavoriteRepository Favorites { get; }
    IAdminReviewLogRepository AdminReviewLogs { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}