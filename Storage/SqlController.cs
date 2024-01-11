namespace Storage;

public interface SqlController
{
    List<IDictionary<string, object>> SelectRows(DatabaseTableName tableName);
    List<IDictionary<string, object>> SelectRows(DatabaseTableName tableName, DatabaseColumnName idColumnName, string Id);
    void InsertRow(DatabaseTableName tableName, DatabaseColumnName[] columnNames, string[] columnValues);
    void DeleteRow(DatabaseTableName tableName, string Id);
    void DeleteRows(DatabaseTableName tableName, DatabaseColumnName idColumnName, string Id);
    void EditRow(DatabaseTableName tableName, DatabaseColumnName updateColumn, string updateColumnValue, DatabaseColumnName whereColumn, string whereValue);
}
