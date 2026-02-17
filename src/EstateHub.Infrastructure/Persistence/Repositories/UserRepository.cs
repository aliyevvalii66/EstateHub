using EstateHub.Application.Interfaces.Repositories;
using EstateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EstateHub.Infrastructure.Persistence.Repositories;

public class UserRepository : BaseRepository<AppUser>, IUserRepository
{
    public UserRepository(EstateHubDbContext context) : base(context) { }

    public async Task<AppUser?> GetByEmailAsync(string email)
        => await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);

    public async Task<List<AppUser>> GetBannedUsersAsync()
        => await _dbSet
            .Where(x => x.IsBanned)
            .AsNoTracking()
            .ToListAsync();
}