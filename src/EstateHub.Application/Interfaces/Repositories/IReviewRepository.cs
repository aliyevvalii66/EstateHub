using EstateHub.Domain.Entities;

namespace EstateHub.Application.Interfaces.Repositories;

public interface IReviewRepository : IRepository<Review>
{
    Task<List<Review>> GetReviewsByListingAsync(Guid listingId);
    Task<double> GetAverageRatingAsync(Guid listingId);
}