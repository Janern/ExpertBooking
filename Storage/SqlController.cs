namespace Storage;

public interface SqlController
{
    List<IDictionary<string, object>> SelectRows(DatabaseTableName tableName);
    void InsertRow(DatabaseTableName tableName, DatabaseColumnName[] columnNames, string[] columnValues);
    void DeleteRow(DatabaseTableName tableName, string Id);
}
