using EstateHub.Domain.Entities;

namespace EstateHub.Application.Interfaces.Repositories;

public interface IBookingRepository : IRepository<Booking>
{
    Task<bool> HasOverlapAsync(Guid listingId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<Booking>> GetBookingsByUserAsync(Guid userId);
}