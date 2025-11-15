using Domain.Shared;
using MediatR;

namespace Application.Auth.Logout;

public record LogoutCommand(string? RefreshToken) : IRequest<Result>;
