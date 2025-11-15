using Domain.Shared;
using MediatR;

namespace Application.Auth.Refresh;

public record RefreshCommand(string? RefreshToken) : IRequest<Result<RefreshResponse>>;
