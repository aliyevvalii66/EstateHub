using EstateHub.Application.Interfaces.Repositories;
using EstateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstateHub.Infrastructure.Persistence.Repositories;

public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
{
    public NotificationRepository(EstateHubDbContext context) : base(context) { }

    public async Task<List<Notification>> GetUnreadByUserAsync(Guid userId)
        => await _dbSet
            .Where(x => x.UserId == userId && !x.IsRead)
            .OrderByDescending(x => x.CreatedAt)
            .AsNoTracking()
            .ToListAsync();

    public async Task MarkAllAsReadAsync(Guid userId)
    {
        var notifications = await _dbSet
            .Where(x => x.UserId == userId && !x.IsRead)
            .ToListAsync();

        notifications.ForEach(x => x.IsRead = true);
    }
}