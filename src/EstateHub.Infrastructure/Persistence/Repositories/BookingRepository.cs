using EstateHub.Application.Interfaces.Repositories;
using EstateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstateHub.Infrastructure.Persistence.Repositories;

public class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
    public BookingRepository(EstateHubDbContext context) : base(context) { }

    public async Task<bool> HasOverlapAsync(Guid listingId, DateTime startDate, DateTime endDate)
        => await _dbSet.AnyAsync(x =>
            x.ListingId == listingId &&
            x.Status == "Confirmed" &&
            x.StartDate < endDate &&
            x.EndDate > startDate);

    public async Task<List<Booking>> GetBookingsByUserAsync(Guid userId)
        => await _dbSet
            .Where(x => x.UserId == userId)
            .Include(x => x.Listing)
            .OrderByDescending(x => x.StartDate)
            .AsNoTracking()
            .ToListAsync();
}