using EstateHub.Domain.Entities;

namespace EstateHub.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<AppUser>
{
    Task<AppUser?> GetByEmailAsync(string email);
    Task<List<AppUser>> GetBannedUsersAsync();
}