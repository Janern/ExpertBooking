using System;
using System.Collections.Generic;
using BusinessModels;
using Sqlite;
using Storage;

namespace Tests.UseCases.Experts;

public class ExpertTests
{
    protected List<Expert> _existingExperts;
    protected SqlController _sqlite;

    public ExpertTests()
    {
        _existingExperts = new List<Expert>();
        string? path = AppDomain.CurrentDomain.BaseDirectory;
        _sqlite = new SqliteController(path+"\\..\\..\\..\\ExpertTests.db");
        SetUpTestDB();
    }

    protected void SetUpTestDB()
    {
        DeleteAllExperts();
        InsertExperts();
    }

    private void DeleteAllExperts()
    {
        var rows = _sqlite.SelectRows(DatabaseTableName.Expert);
        foreach(var row in rows)
        {   
            string id = ConvertFromDBVal<string>(row["Id"]);
            _sqlite.DeleteRow(DatabaseTableName.Expert, id);
        }
    }

    private static T ConvertFromDBVal<T>(object obj)
    {
        if (obj == null || obj == DBNull.Value)
        {
            return default(T);
        }
        else
        {
            return (T)obj;
        }
    }

    private void InsertExperts()
    {
        foreach(var expert in _existingExperts)
        {
            _sqlite.InsertRow(DatabaseTableName.Expert, new DatabaseColumnName[]{
                    DatabaseColumnName.Id, 
                    DatabaseColumnName.Description,
                    DatabaseColumnName.FirstName,
                    DatabaseColumnName.LastName,
                    DatabaseColumnName.Role,
                    DatabaseColumnName.Technology
                }, 
                new string[]
                {
                    expert.Id, 
                    expert.Description,
                    expert.FirstName,
                    expert.LastName,
                    expert.Role,
                    expert.Technology
                });
        }
    }
}
