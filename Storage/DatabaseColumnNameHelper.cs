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
            default :
                throw new NotSupportedException("Unknown columnName "+columnName);
        }
    }   
}
