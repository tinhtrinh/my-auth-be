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
        var users = await _getUsersQueryService.GetUsers(query.SearchTerm, query.SortColumn, query.SortOrder);

        var totalCount = await _getUsersQueryService.CountAsync();

        var filteredCount = await _getUsersQueryService.FilteredCountAsync();

        var response = new GetUsersResponse(users, query.PageNumber, query.PageSize, totalCount, filteredCount);

        return Result.Success(response);
    }
}