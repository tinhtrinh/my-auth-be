namespace Persistence.Reports.GetReportDetail;

public class GetReportDetailService
{
    private readonly MyAuthDbContext _dbContext;

    public GetReportDetailService(MyAuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetReportDto> GetReportDetailAsync(
        List<ReportField> reportFields,
        List<ReportSource> reportSources)
    {
        var select = "SELECT";
        foreach(var field in reportFields)
        {
            select += $", { field.Name } AS { field.QueryName }";
        }

        var from = " FROM";
        if (reportSources.Count == 1)
        {
            from += $" { reportSources[0].QueryName }";
        }

        if(reportSources.Count > 1)
        {
            List<ReportDataSourceJoin> reportJoins = [];

            from += $" { reportSources[0] }";

            for(var i = 1; i < reportSources.Count; i++)
            {
                from += $" LEFT JOIN { reportSources[i].QueryName } ON { reportJoins[i - 1].JoinQuery }";
            }
        }

        var where = "";

        var query = select + from + where;

        return new GetReportDto();
    }
}

public record ReportField(int Id, string Name, string QueryName);

public record ReportSource(int Id, string Name, string QueryName);

public record GetReportDetailRequest(
    List<ReportField> ReportFields,
    List<string> Models);

public class ReportDataSource
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public string QueryName { get; private set; }

    public ICollection<ReportDataField> ReportDataFields { get; private set; }

    public ICollection<ReportDataSourceJoin> ReportDataSourceJoins { get; private set; }
}

public class ReportDataField
{
    public int ReportDataSourceId { get; private set; }

    public string Name { get; private set; }

    public string QueryName { get; private set; }

    public ReportDataSource ReportDataSource { get; private set; }
}

public class ReportDataSourceJoin
{
    public int LeftReportDataSourceId { get; private set; }

    public int RightReportDataSourceId { get; private set; }

    public string JoinQuery { get; private set; }

    public ReportDataSource LeftReportDataSource { get; private set; }

    public ReportDataSource RightReportDataSource { get; private set; }
}

public class ReportFilter { }

public record GetReportDto
{

}