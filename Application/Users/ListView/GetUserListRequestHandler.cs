using Domain.Shared;
using MediatR;

namespace Application.Users.ListView;

internal sealed class GetUserListRequestHandler : IRequestHandler<GetUserListRequest, Result<GetUserListResponse>>
{
    private readonly IGetUserListService _getUserListService;

    public GetUserListRequestHandler(IGetUserListService getUserListService)
    {
        _getUserListService = getUserListService;
    }

    public async Task<Result<GetUserListResponse>> Handle(GetUserListRequest request, CancellationToken cancellationToken)
    {
        var userList = await _getUserListService.GetUserListAsync(
            request.Columns,
            request.Filter,
            request.SearchTerm,
            request.SearchColumn,
            request.SortColumn,
            request.SortOrder,
            request.PageNumber, 
            request.PageSize);

        var totalCount = await _getUserListService.CountAsync();

        var filteredCount = await _getUserListService.FilteredCountAsync();

        var response = new GetUserListResponse(userList, request.PageNumber, request.PageSize, totalCount, filteredCount);

        return Result.Success(response);
    }
}
