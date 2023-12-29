namespace Storage;

public static class DatabaseTableNameHelper
{
    public static string GetTableName(DatabaseTableName tableName)
    {
        switch(tableName){
            case DatabaseTableName.Expert :
                return "Expert";
            case DatabaseTableName.Cart :
                return "Cart";
            case DatabaseTableName.CartExpert :
                return "CartExpert";
            default :
                throw new NotSupportedException("Unknown tablename "+tableName);
        }
    }   
}
