using Microsoft.Data.Sqlite;
using Storage;

namespace Sqlite;
public class SqliteController : SqlController
{
    private readonly string _databaseName;
    public SqliteController(string databaseName)
    {
        if (!File.Exists(databaseName)){
            throw new Exception(databaseName+" DB IS MISSING!");
        }else{
            Console.WriteLine(databaseName + " DB FOUND!");
        }
        _databaseName = databaseName;
    }

    public void InsertRow(DatabaseTableName tableName, DatabaseColumnName[] columnNames, string[] columnValues)
    {
        using (var connection = new SqliteConnection($"Data Source={_databaseName}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            string columnNamesText = string.Join(", ", columnNames.Select(col => DatabaseColumnNameHelper.GetColumnName(col)));
            string parameterNamesText = string.Join(", ", columnValues.Select((_, index) => $"@ColumnValue{index}"));
            command.CommandText =
            $@"
                INSERT INTO {DatabaseTableNameHelper.GetTableName(tableName)} ({columnNamesText})
                VALUES ({parameterNamesText})";
            for (int i = 0; i < columnValues.Length; i++)
            {
                command.Parameters.AddWithValue($"@ColumnValue{i}", columnValues[i]);
            }
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

    public List<IDictionary<string, object>> SelectRows(DatabaseTableName tableName, DatabaseColumnName idColumnName, string Id) 
    {
        using (var connection = new SqliteConnection($"Data Source={_databaseName}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
            $@"
                SELECT * 
                FROM {DatabaseTableNameHelper.GetTableName(tableName)} 
                WHERE {DatabaseColumnNameHelper.GetColumnName(idColumnName)} = @Id             
            ";
            command.Parameters.AddWithValue("@Id", Id);
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

    public void DeleteRows(DatabaseTableName tableName, DatabaseColumnName idColumnName, string Id)
    {
        using (var connection = new SqliteConnection($"Data Source={_databaseName}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
            $@"
                DELETE FROM {DatabaseTableNameHelper.GetTableName(tableName)} WHERE {DatabaseColumnNameHelper.GetColumnName(idColumnName)} = @Id
            ";
            command.Parameters.AddWithValue("@Id", Id);
            command.ExecuteNonQuery();
        }
    }

    public void EditRow(DatabaseTableName tableName, Dictionary<DatabaseColumnName, string> updateColumns, DatabaseColumnName whereColumn, string whereValue)
    {
        using (var connection = new SqliteConnection($"Data Source={_databaseName}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
            $"UPDATE {DatabaseTableNameHelper.GetTableName(tableName)} "+
            CreateUpdateStringWithParamaterNames(updateColumns) + " " +           
            $"WHERE {DatabaseColumnNameHelper.GetColumnName(whereColumn)} = @WhereValue";
            AddParameterValuesForUpdate(command.Parameters, updateColumns);
            command.Parameters.AddWithValue("@WhereValue", whereValue);
            command.ExecuteNonQuery();
        }
    }

    private string CreateUpdateStringWithParamaterNames(Dictionary<DatabaseColumnName, string> updateColumns)
    {
        string result = "SET ";
        DatabaseColumnName[] keys = updateColumns.Keys.ToArray();
        for(int i = 0; i < keys.Length-1; i++)
        {
            result += DatabaseColumnNameHelper.GetColumnName(keys[i]) + " = @" + DatabaseColumnNameHelper.GetColumnName(keys[i]) + ", ";
        }
        result += DatabaseColumnNameHelper.GetColumnName(keys[keys.Length-1]) + " = @" + DatabaseColumnNameHelper.GetColumnName(keys[keys.Length-1]);
        Console.WriteLine("updatestring with parameternames: "+result);
        return result;
    }

    private void AddParameterValuesForUpdate(SqliteParameterCollection collection, Dictionary<DatabaseColumnName, string> updateColumns)
    {
        DatabaseColumnName[] keys = updateColumns.Keys.ToArray();
        for(int i = 0; i < keys.Length; i++)
        {
            Console.WriteLine("setting parametervalue: @"+DatabaseColumnNameHelper.GetColumnName(keys[i])+":"+updateColumns[keys[i]]);
            collection.AddWithValue("@"+DatabaseColumnNameHelper.GetColumnName(keys[i]), updateColumns[keys[i]]);
        }
    }
}