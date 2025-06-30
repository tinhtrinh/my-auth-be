namespace Application.Shared.ListView;

public class FilterCondition
{
    public string Column { get; private set; }

    public string Type { get; private set; }

    public string Operator { get; private set; }

    public ICollection<string> Values { get; private set; }

    public FilterCondition(string column, string type, string @operator, ICollection<string> values)
    {
        Column = column;
        Type = type;
        Operator = @operator;
        Values = values;
    }
}
