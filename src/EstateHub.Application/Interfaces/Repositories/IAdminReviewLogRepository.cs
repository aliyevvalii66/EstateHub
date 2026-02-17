using EstateHub.Domain.Entities;

namespace EstateHub.Application.Interfaces.Repositories;

public interface IAdminReviewLogRepository : IRepository<AdminReviewLog>
{
    Task<IEnumerable<AdminReviewLog>> GetLogsByListingAsync(Guid listingId);
    Task<IEnumerable<AdminReviewLog>> GetLogsByAdminAsync(Guid adminId);
}