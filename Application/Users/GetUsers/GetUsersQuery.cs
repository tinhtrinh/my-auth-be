using Application.Shared.Pagination;
using Domain.Shared;
using MediatR;

namespace Application.Users.GetUsers;

public record GetUsersQuery(GetUsersRequest Request) : PagedQuery(Request), IRequest<Result<GetUsersResponse>>;