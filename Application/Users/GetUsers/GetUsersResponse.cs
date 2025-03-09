using Application.Shared;
using Domain.Users;

namespace Application.Users.GetUsers;

public class GetUsersResponse : PagedResponse<GetUsersDTO>
{
    public string? PlaceholderToTestExtra { get; private set; }

    public static async Task<GetUsersResponse> CreateAsync(IQueryable<GetUsersDTO> query, int? pageNumber, int? pageSize)
    {
        var response = await CreateChildAsync<GetUsersResponse>(query, pageNumber, pageSize);
        response.PlaceholderToTestExtra = "This is a placeholder to test the extension of class.";
        return response;
    }
}