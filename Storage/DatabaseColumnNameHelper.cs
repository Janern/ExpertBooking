namespace Storage;

public static class DatabaseColumnNameHelper
{
    public static string GetColumnName(DatabaseColumnName columnName)
    {
        switch(columnName){
            case DatabaseColumnName.Id :
                return "Id";
            default :
                throw new NotSupportedException("Unknown columnName "+columnName);
        }
    }   
}
