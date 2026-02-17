using EstateHub.Application.Interfaces.Repositories;
using EstateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstateHub.Infrastructure.Persistence.Repositories;

public class ReviewRepository : BaseRepository<Review>, IReviewRepository
{
    public ReviewRepository(EstateHubDbContext context) : base(context) { }

    public async Task<List<Review>> GetReviewsByListingAsync(Guid listingId)
        => await _dbSet
            .Where(x => x.ListingId == listingId)
            .OrderByDescending(x => x.CreatedAt)
            .AsNoTracking()
            .ToListAsync();

    public async Task<double> GetAverageRatingAsync(Guid listingId)
    {
        var reviews = await _dbSet
            .Where(x => x.ListingId == listingId)
            .AsNoTracking()
            .ToListAsync();

        return reviews.Any() ? reviews.Average(x => x.Rating) : 0;
    }
}