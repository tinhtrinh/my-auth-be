namespace Application.Shared.Export;

public interface IExportService
{
    Task<string> ExportCsvAsync<T>(List<T> list);

    void ReportProgress(Action<int> callback);
}
