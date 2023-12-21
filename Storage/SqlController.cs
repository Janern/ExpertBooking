namespace Storage;

public interface SqlController
{
    List<IDictionary<string, object>> SelectRows(DatabaseTableName tableName);
}
