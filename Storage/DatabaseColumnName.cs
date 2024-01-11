using System.ComponentModel;

namespace Storage;

/*
* This enum is used to reference database column names
* to avoid any possibility of sqlinjection when interpolating strings
* in sql-commands
*/
public enum DatabaseColumnName
{
    Id,
    CartId,
    ExpertId,
    Description,
    FirstName,
    LastName,
    Role, 
    Technology
}