using EstateHub.Application.Interfaces;
using EstateHub.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace EstateHub.Infrastructure.Persistence.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly EstateHubDbContext _context;
    private IDbContextTransaction? _transaction;

    private IListingRepository? _listings;
    private IBookingRepository? _bookings;
    private IUserRepository? _users;
    private IReviewRepository? _reviews;
    private INotificationRepository? _notifications;
    private IFavoriteRepository? _favorites;
    private IAdminReviewLogRepository? _adminReviewLogs;

    public RepositoryManager(EstateHubDbContext context)
    {
        _context = context;
    }

    public IListingRepository Listings
        => _listings ??= new ListingRepository(_context);

    public IBookingRepository Bookings
        => _bookings ??= new BookingRepository(_context);

    public IUserRepository Users
        => _users ??= new UserRepository(_context);

    public IReviewRepository Reviews
        => _reviews ??= new ReviewRepository(_context);

    public INotificationRepository Notifications
        => _notifications ??= new NotificationRepository(_context);

    public IFavoriteRepository Favorites
        => _favorites ??= new FavoriteRepository(_context);

    public IAdminReviewLogRepository AdminReviewLogs
        => _adminReviewLogs ??= new AdminReviewLogRepository(_context);

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();

    public async Task BeginTransactionAsync()
        => _transaction = await _context.Database.BeginTransactionAsync();

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}