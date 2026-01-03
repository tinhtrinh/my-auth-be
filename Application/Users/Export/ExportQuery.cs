using Domain.Shared;
using MediatR;

namespace Application.Users.Export;

public record ExportQuery(string ConnectionId, string? Token) : IRequest<Result>;
