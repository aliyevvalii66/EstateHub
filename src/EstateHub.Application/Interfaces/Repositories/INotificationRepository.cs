using EstateHub.Domain.Entities;

namespace EstateHub.Application.Interfaces.Repositories;

public interface INotificationRepository : IRepository<Notification>
{
    Task<List<Notification>> GetUnreadByUserAsync(Guid userId);
    Task MarkAllAsReadAsync(Guid userId);
}