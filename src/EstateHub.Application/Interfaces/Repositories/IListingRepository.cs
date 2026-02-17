using EstateHub.Domain.Entities;

namespace EstateHub.Application.Interfaces.Repositories;

public interface IListingRepository : IRepository<Listing>
{
    Task<List<Listing>> GetPendingListingsAsync();
    Task<List<Listing>> GetListingsByOwnerAsync(Guid ownerId);
    Task<Listing?> GetListingWithDetailsAsync(Guid id);
}