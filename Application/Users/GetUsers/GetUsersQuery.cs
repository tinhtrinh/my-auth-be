using Application.Shared;
using Domain.Shared;
using Domain.Users;
using MediatR;

namespace Application.Users.GetUsers;

public record GetUsersQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int? PageNumber,
    int? PageSize) : IRequest<Result<GetUsersResponse>>;