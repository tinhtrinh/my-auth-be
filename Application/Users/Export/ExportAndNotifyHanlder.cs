using Application.Shared.Export;
using Application.Shared.RealTime;

namespace Application.Users.Export;

public class ExportAndNotifyHanlder
{
    private readonly IExportService _exportService;
    private readonly IRealTimeService _realTimeService;

    public ExportAndNotifyHanlder(IExportService exportService, IRealTimeService realTimeService)
    {
        _exportService = exportService;
        _realTimeService = realTimeService;
    }

    public async Task ExcuteAsync(string connectionId, string userId, CancellationToken cancellationToken) 
    {
        _exportService.ReportProgress(async (percent) =>
        {
            await _realTimeService.SendToConnectionAsync(
                connectionId,
                "UpdatedExportProgress",
                percent,
                cancellationToken);
        });

        var result = await _exportService.ExportCsvAsync<string>([]);

        await _realTimeService.SendToUserAsync(
            userId,
            "ReceivedExportResult",
            result,
            cancellationToken);
    }
}
