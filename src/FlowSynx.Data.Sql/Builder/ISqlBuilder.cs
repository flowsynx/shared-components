namespace FlowSynx.Data.Sql.Builder;

public interface ISqlBuilder
{
    string Create(Format format, CreateOption option);
    string Select(Format format, SelectOption option);
    string Insert(Format format, InsertOption option);
}