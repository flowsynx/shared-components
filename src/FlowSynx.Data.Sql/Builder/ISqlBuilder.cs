namespace FlowSynx.Data.Sql.Builder;

public interface ISqlBuilder
{
    string Create(Format format, CreateOption option);
    string Select(Format format, SelectOption option);
    string ExistRecord(Format format, ExistRecordOption option);
    string ExistTable(Format format, ExistTableOption option);
    string Insert(Format format, InsertOption option);
    string Delete(Format format, DeleteOption option);
    string DropTable(Format format, DropTableOption option);
    string TableFields(Format format, TableFieldsOption option);
}