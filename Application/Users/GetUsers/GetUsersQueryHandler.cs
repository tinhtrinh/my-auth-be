using Domain.Shared;
using MediatR;

namespace Application.Users.GetUsers;

internal sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<GetUsersResponse>>
{
    private readonly IGetUsersQueryService _getUsersQueryService;

    public GetUsersQueryHandler(IGetUsersQueryService getUsersQueryService)
    {
        _getUsersQueryService = getUsersQueryService;
    }

    public async Task<Result<GetUsersResponse>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = _getUsersQueryService.GetUsers(query.SearchTerm, query.SortColumn, query.SortOrder);

        var response = await GetUsersResponse.CreateAsync(users, query.PageNumber, query.PageSize);

        return Result.Success(response);
    }
}