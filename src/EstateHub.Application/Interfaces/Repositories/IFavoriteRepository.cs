using EstateHub.Domain.Entities;

namespace EstateHub.Application.Interfaces.Repositories;

public interface IFavoriteRepository : IRepository<Favorite>
{
    Task<List<Favorite>> GetFavoritesByUserAsync(Guid userId);
    Task<bool> IsFavoriteAsync(Guid userId, Guid listingId);
}