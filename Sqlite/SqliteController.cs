using System.ComponentModel;
using Microsoft.Data.Sqlite;

namespace Sqlite;
public class SqliteController
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

    public List<IDictionary<string, object>> SelectRows(string tableName) 
    {
       using (var connection = new SqliteConnection($"Data Source={_databaseName}"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
            @"
                SELECT *
                FROM Expert
            ";
            List<IDictionary<string, object>> rows = new List<IDictionary<string, object>>();
            command.Parameters.AddWithValue("tableName", tableName);
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
}
