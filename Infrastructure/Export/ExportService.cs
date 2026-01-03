using Application.Shared.Export;

namespace Infrastructure.Export;

public class ExportService : IExportService
{
    private Action<int>? _progressCallback;

    public async Task<string> ExportCsvAsync<T>(List<T> list)
    {
        for (int percent = 0; percent <= 100; percent += 25)
        {
            _progressCallback?.Invoke(percent);
            await Task.Delay(5000);
        }

        return "Export Done";
    }

    public void ReportProgress(Action<int> callback)
    {
        _progressCallback = callback;
    }
}
