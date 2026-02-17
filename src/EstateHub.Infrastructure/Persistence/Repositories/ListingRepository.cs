using EstateHub.Application.Interfaces.Repositories;
using EstateHub.Domain.Entities;
using EstateHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EstateHub.Infrastructure.Persistence.Repositories;

public class ListingRepository : BaseRepository<Listing>, IListingRepository
{
    public ListingRepository(EstateHubDbContext context) : base(context) { }

    public async Task<List<Listing>> GetPendingListingsAsync()
        => await _dbSet
            .Where(x => x.Status == ListingStatus.Pending)
            .OrderBy(x => x.CreatedAt)
            .AsNoTracking()
            .ToListAsync();

    public async Task<List<Listing>> GetListingsByOwnerAsync(Guid ownerId)
        => await _dbSet
            .Where(x => x.OwnerId == ownerId)
            .OrderByDescending(x => x.CreatedAt)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Listing?> GetListingWithDetailsAsync(Guid id)
        => await _dbSet
            .Include(x => x.Detail)
            .Include(x => x.Location)
            .Include(x => x.Images)
            .Include(x => x.Reviews)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
}