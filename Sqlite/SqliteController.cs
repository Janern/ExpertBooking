using Microsoft.Data.Sqlite;
using Storage;

namespace Sqlite;
public class SqliteController : SqlController
{
    private readonly string _databaseName;
    public SqliteController(string databaseName)
    {
        if (!File.Exists(databaseName)){
            throw new Exception("DB IS MISSING!");
        }else{
            Console.WriteLine("DB FOUND!");
        }
        _databaseName = databaseName;
    }

    public void InsertRow(DatabaseTableName tableName, DatabaseColumnName[] columnNames, string[] columnValues)
    {
        using (var connection = new SqliteConnection($"Data Source={_databaseName}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
            $@"
                INSERT INTO {DatabaseTableNameHelper.GetTableName(tableName)}
                (
                    "+
                        columnNames.Select(col => DatabaseColumnNameHelper.GetColumnName(col)).Aggregate((a, b) => a + ", " + b)
                    +
            @"
                )
                VALUES
                (@ColumnValues)
            ";
            command.Parameters.AddWithValue("@ColumnValues", columnValues.Aggregate((a, b) => a + ", " + b));
            command.ExecuteNonQuery();
        }
    }

    public List<IDictionary<string, object>> SelectRows(DatabaseTableName tableName) 
    {
        using (var connection = new SqliteConnection($"Data Source={_databaseName}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
            $@"
                SELECT *
                FROM {DatabaseTableNameHelper.GetTableName(tableName)}
            ";
            List<IDictionary<string, object>> rows = new List<IDictionary<string, object>>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var row = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[reader.GetName(i)] = reader.GetValue(i);
                    }

                    rows.Add(row);  
                }
            }
            return rows;
        }
    }
    
    public void DeleteRow(DatabaseTableName tableName, string Id)
    {
        using (var connection = new SqliteConnection($"Data Source={_databaseName}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
            $@"
                DELETE FROM {DatabaseTableNameHelper.GetTableName(tableName)} WHERE Id = @Id
            ";
            command.Parameters.AddWithValue("@Id", Id);
            command.ExecuteNonQuery();
        }
    }
}