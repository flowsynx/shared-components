namespace FlowSynx.Data.Sql.Builder;

public interface ISqlBuilder
{
    string Create(Format format, CreateOption option);
    string Select(Format format, SelectOption option);
    string Exist(Format format, ExistOption option);
    string Insert(Format format, InsertOption option);
    string Delete(Format format, DeleteOption option);
    string DropTable(Format format, DropTableOption option);
}