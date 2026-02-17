using EstateHub.Application.Common.Models;
using MediatR;

namespace EstateHub.Application.Features.Auth.Commands.Login;

public record LoginCommand : IRequest<Result<AuthResponseDto>>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}