using EstateHub.Application.Interfaces.Repositories;
using EstateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstateHub.Infrastructure.Persistence.Repositories;

public class AdminReviewLogRepository : BaseRepository<AdminReviewLog>, IAdminReviewLogRepository
{
    public AdminReviewLogRepository(EstateHubDbContext context) : base(context) { }

    public async Task<List<AdminReviewLog>> GetLogsByListingAsync(Guid listingId)
        => await _dbSet
            .Where(x => x.ListingId == listingId)
            .OrderByDescending(x => x.ReviewedAt)
            .AsNoTracking()
            .ToListAsync();

    public async Task<List<AdminReviewLog>> GetLogsByAdminAsync(Guid adminId)
        => await _dbSet
            .Where(x => x.AdminId == adminId)
            .OrderByDescending(x => x.ReviewedAt)
            .AsNoTracking()
            .ToListAsync();
}