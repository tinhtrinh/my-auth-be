namespace Application.Shared.Pagination;

public interface ICountDataService
{
    Task<int> CountAsync();

    Task<int> FilteredCountAsync();
}
