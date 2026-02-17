using EstateHub.Domain.Entities;

namespace EstateHub.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(AppUser user, IList<string> roles);
    string GenerateRefreshToken();
    DateTime GetAccessTokenExpiry();
}