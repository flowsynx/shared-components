namespace FlowSynx.Data.Sql.Builder;

public interface ISqlBuilder
{
    string Select(Format format, SelectOption option);
    string Insert(Format format, InsertOption option);
}