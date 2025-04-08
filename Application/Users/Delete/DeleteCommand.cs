using Domain.Shared;
using MediatR;

namespace Application.Users.Delete;

public record DeleteCommand(string Id) : IRequest<Result>;