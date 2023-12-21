using System.ComponentModel;

namespace Storage;

/*
* This enum is used to reference database table names
* to avoid any possibility of sqlinjection when interpolating strings
* in sql-commands
*/
public enum DatabaseTableName
{
    [Description("Expert")]
    Expert,

    [Description("Cart")]
    Cart,
    [Description("CartExpert")]
    CartExpert,
}