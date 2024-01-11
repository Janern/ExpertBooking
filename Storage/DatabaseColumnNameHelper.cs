namespace Storage;

public static class DatabaseColumnNameHelper
{
    public static string GetColumnName(DatabaseColumnName columnName)
    {
        switch(columnName){
            case DatabaseColumnName.Id :
                return "Id";
            case DatabaseColumnName.CartId :
                return "CartId";
            case DatabaseColumnName.ExpertId :
                return "ExpertId";
            case DatabaseColumnName.Description :
                return "Description";
            case DatabaseColumnName.FirstName :
                return "FirstName";
            case DatabaseColumnName.LastName :
                return "LastName";
            case DatabaseColumnName.Role :
                return "Role";
            case DatabaseColumnName.Technology :
                return "Technology";
            default :
                throw new NotSupportedException("Unknown columnName "+columnName);
        }
    }   
}
