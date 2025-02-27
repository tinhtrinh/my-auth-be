using Application.Shared;
using Domain.Shared;
using Domain.Users;
using MediatR;

namespace Application.Users.GetUsers;

internal sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<GetUsersResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<GetUsersResponse>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetUsers(query.SearchTerm, query.SortColumn, query.SortOrder);

        var response = GetUsersResponse.CreateAsync(users, query.PageNumber, query.PageSize);

        return Result.Success(response);
    }
}