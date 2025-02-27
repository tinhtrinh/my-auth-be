using Application.Shared;
using Domain.Users;

namespace Application.Users.GetUsers;

public class GetUsersResponse : PagedResponse<GetUsersDTO>
{
    private GetUsersResponse(IEnumerable<GetUsersDTO> query, int? pageNumber, int? pageSize)
        : base(query, pageNumber, pageSize)
    {

    }

    public static GetUsersResponse CreateAsync(List<User> users, int? pageNumber, int? pageSize)
    {
        var getUsersDTOs = users.Select(users => GetUsersDTO.Create(users));
        return new GetUsersResponse(getUsersDTOs, pageNumber, pageSize);
    }
}