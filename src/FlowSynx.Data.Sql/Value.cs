namespace FlowSynx.Data.Sql;

public class Value
{
    public string Expression { get; set; }

    public Value(string expression)
    {
        Expression = expression;
    }
}