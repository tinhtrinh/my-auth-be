using Application.ListViews.Shared;
using Domain.Shared;
using MediatR;

namespace Application.Users.ListView;

public record GetUserListRequest : GetListDataRequest, IRequest<Result<GetUserListResponse>>
{
    public GetUserListRequest(
        List<string> columns, 
        List<FilterCondition>? filter, 
        string? SearchTerm, 
        string? SearchColumn, 
        string? SortColumn, 
        string? SortOrder, 
        int? PageNumber, 
        int? PageSize) : base(
            columns, 
            filter, 
            SearchTerm, 
            SearchColumn, 
            SortColumn, 
            SortOrder, 
            PageNumber, 
            PageSize)
    {
    }
}