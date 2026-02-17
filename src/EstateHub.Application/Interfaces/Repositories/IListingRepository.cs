using EstateHub.Domain.Entities;

namespace EstateHub.Application.Interfaces.Repositories;

public interface IListingRepository : IRepository<Listing>
{
    Task<IEnumerable<Listing>> GetPendingListingsAsync();
    Task<IEnumerable<Listing>> GetListingsByOwnerAsync(Guid ownerId);
    Task<Listing?> GetListingWithDetailsAsync(Guid id);
}