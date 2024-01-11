using BusinessModels;
using UseCases.Experts;


namespace Storage;

public class ExpertStorageSqliteImplementation : ExpertsStorage
{
    private SqlController _sqlite;

    public ExpertStorageSqliteImplementation(SqlController sqlite)
    {
        _sqlite = sqlite;
    }

    public void EditExpert(EditExpertRequest request)
    {
        if(request.Description != null)
            _experts[request.Id].Description = request.Description;
    }

    public bool Exists(string id)
    {
        List<IDictionary<string, object>> rows = _sqlite.SelectRows(DatabaseTableName.Expert, DatabaseColumnName.Id, id);
        return rows.Any();
    }

    public Expert GetExpert(string id)
    {
        List<IDictionary<string, object>> rows = _sqlite.SelectRows(DatabaseTableName.Expert);
        IDictionary<string, object>? row = rows.FirstOrDefault(r => ((string) r["Id"]) == id);
        return new Expert{
            Id = (string) row["Id"],
            FirstName = (string)row["FirstName"],
            LastName = (string)row["LastName"],
            Role = (string)row["Role"],
            Description = (string)row["Description"],
            Technology = (string)row["Technology"]
        };
    }

    public Expert[] GetExperts(string technologyFilter, string[] expertIds = null)
    {
        List<IDictionary<string, object>>? rows = _sqlite.SelectRows(DatabaseTableName.Expert);
        return rows.Select(r => new Expert
        {
            Id = (string) r["Id"],
            FirstName = (string)r["FirstName"],
            LastName = (string)r["LastName"],
            Role = (string)r["Role"],
            Description = (string)r["Description"],
            Technology = (string)r["Technology"]

        }).ToArray();
    }
}
