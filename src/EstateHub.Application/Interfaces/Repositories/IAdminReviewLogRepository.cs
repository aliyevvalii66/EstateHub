using EstateHub.Domain.Entities;

namespace EstateHub.Application.Interfaces.Repositories;

public interface IAdminReviewLogRepository : IRepository<AdminReviewLog>
{
    Task<List<AdminReviewLog>> GetLogsByListingAsync(Guid listingId);
    Task<List<AdminReviewLog>> GetLogsByAdminAsync(Guid adminId);
}