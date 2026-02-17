using EstateHub.Application.Interfaces.Repositories;
using EstateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstateHub.Infrastructure.Persistence.Repositories;

public class FavoriteRepository : BaseRepository<Favorite>, IFavoriteRepository
{
    public FavoriteRepository(EstateHubDbContext context) : base(context) { }

    public async Task<List<Favorite>> GetFavoritesByUserAsync(Guid userId)
        => await _dbSet
            .Where(x => x.UserId == userId)
            .Include(x => x.Listing)
            .AsNoTracking()
            .ToListAsync();

    public async Task<bool> IsFavoriteAsync(Guid userId, Guid listingId)
        => await _dbSet.AnyAsync(x =>
            x.UserId == userId &&
            x.ListingId == listingId);
}