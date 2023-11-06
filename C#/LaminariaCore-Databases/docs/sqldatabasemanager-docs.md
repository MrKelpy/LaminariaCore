# SQLDatabaseManager Class Documentation

## Overview
The `SQLDatabaseManager` class is used to manage one or multiple SQL Server databases. This class is designed to work in conjunction with an `SQLServerConnector` object, allowing you to manage the databases within it.

## Constructors

### `SQLDatabaseManager(SQLServerConnector connector)`
- Initializes a new instance of the `SQLDatabaseManager` class.
- Binds the manager to an `SQLServerConnector` object, which will be used to interact with the databases.
- **Parameters:**
    - `connector` (SQLServerConnector): The SQLServerConnector object to use for database management.

## Methods

### `UseDatabase(string name) -> bool`
- Switches the currently active database to the one specified.
- **Parameters:**
    - `name` (string): The name of the database to use.
- **Returns:**
    - `bool`: `true` if the database switch was successful, `false` if there was an error.

### `ExportDatabase(string name, string filepath)`
- Exports the specified database to the specified file path.
- **Parameters:**
    - `name` (string): The name of the database to export.
    - `filepath` (string): The path to save the backup to.

### `ImportDatabase(string name, string filepath) -> int`
- Imports the specified database from the specified file path.
- **Parameters:**
    - `name` (string): The name of the database to restore.
    - `filepath` (string): The path to restore the backup from.
- **Returns:**
    - `int`: The number of rows affected by the import.

### `InsertInto(string table, string[] fields, params dynamic[] values) -> bool`
- Performs a composite INSERT INTO query into the specified table, restricting the addition to specified fields and values.
- **Parameters:**
    - `table` (string): The table to add the content into.
    - `fields` (string[]): The fields restricting the values to be added into the table.
    - `values` (dynamic[]): The values matching the restriction fields.
- **Returns:**
    - `bool`: `true` if changes were made in the database, `false` otherwise.

### `InsertInto(string table, params dynamic[] values) -> bool`
- Performs a simple INSERT INTO query into the specified table with the specified values.
- This method assumes that you are inserting into all of the columns in the table.
- **Parameters:**
    - `table` (string): The table to add the values into.
    - `values` (dynamic[]): The values to add into the table.
- **Returns:**
    - `bool`: `true` if changes were made in the database, `false` otherwise.

### `DeleteFrom(string table, string condition) -> int`
- Performs a DELETE FROM query into the specified table with the specified condition.
- **Parameters:**
    - `table` (string): The table to delete data from.
    - `condition` (string): A string specifying the condition for the entries to be deleted.
- **Returns:**
    - `int`: The number of rows affected by the delete operation.

### `Update(string table, string fieldToUpdate, dynamic value, string condition) -> int`
- Updates all the fields in a specified table with the specified value, based on a condition.
- **Parameters:**
    - `table` (string): The table to update the field of.
    - `fieldToUpdate` (string): The field to be updated.
    - `value` (dynamic): The value to add to the field.
    - `condition` (string): An optional search condition to narrow down field entries.
- **Returns:**
    - `int`: The number of rows affected by the update.

### `Update(string table, string fieldToUpdate, dynamic value) -> int`
- Updates all the fields in a specified table with the specified value.
- **Parameters:**
    - `table` (string): The table to update the field of.
    - `fieldToUpdate` (string): The field to be updated.
    - `value` (dynamic): The value to add to the field.
- **Returns:**
    - `int`: The number of rows affected by the update.

### `Select(string[] fields, string table, string condition) -> List<string[]>`
- Performs a SELECT query into the specified table with the specified fields and condition.
- **Parameters:**
    - `fields` (string[]): The fields to select from the table.
    - `table` (string): The table to select the fields from.
    - `condition` (string): The condition to narrow down the results.
- **Returns:**
    - `List<string[]>`: A matrix containing the results of the query.

### `Select(string[] fields, string table) -> List<string[]>`
- Performs a SELECT query into the specified table with the specified fields.
- **Parameters:**
    - `fields` (string[]): The fields to select from the table.
    - `table` (string): The table to select the fields from.
- **Returns:**
    - `List<string[]>`: A matrix containing the results of the query.

### `Select(string table, string condition) -> List<string[]>`
- Performs a 'SELECT *' query into the specified table with the specified condition.
- **Parameters:**
    - `table` (string): The table to select the fields from.
    - `condition` (string): The condition to narrow down the results.
- **Returns:**
    - `List<string[]>`: A matrix containing the results of the query.

### `Select(string table) -> List<string[]>`
- Performs a 'SELECT *' query into the specified table.
- **Parameters:**
    - `table` (string): The table to select all fields from.
- **Returns:**
    - `List<string[]>`: A matrix containing the results of the query.

### `SendQuery(string query) -> List<string[]>`
- Sends a SQL query to the connected database.
- **Parameters:**
    - `query` (string): The MSSQL statement to be sent to the database for querying.
- **Returns:**
    - `List<string[]>`: A matrix with the results of the query.

### `SendNonQuery(string query) -> int`
- Sends a non-query (no expected results) instruction to the database.
- **Parameters:**
    - `query` (string): The query to send.
- **Returns:**
    - `int`: The number of rows affected by the operation.

### `RunSqlScript(string filepath) -> int`
- Reads the content of an SQL script and executes it through a transaction, separated by "GO" statements.
- **Parameters:**
    - `filepath` (string): The filepath to the SQL script.
- **Returns:**
    - `int`: The total number of rows affected by executing the script.

### `GetColumnsForTable(string tableName) -> string[]`
- Returns an array containing the names of all the columns in the specified table.
- **Parameters:**
    - `tableName` (string): The name of the table to get the columns for.
- **Returns:**
    - `string[]`: An array of column names for the table.

### `ArrayToQueryString(params string[] array) -> string`
- Converts an array of strings into a query string, with each element separated by a comma.
- **Parameters:**
    - `array` (string[]): The array to be converted into a query string.
- **Returns:**
    - `string`: The query string to be used to specify query objects.

### `CreatePlaceholdersArrayString(int arraySize) -> string`
- Creates placeholders for a specified array to be used in a query.
- The placeholders are in the format of @p0, @p1, @p2, etc, to be replaced by the actual values.
- **Parameters:**
    - `arraySize` (int): The size of the array to convert into placeholders.
- **Returns:**
    - `string`: The string containing the placeholders for the array.

### `AddValuesIntoCommand(SqlCommand command, params dynamic[] values)`
- Adds the values into the specified SqlCommand as parameters, with placeholders in the format of @p0, @p1, @p2, etc.
- **Parameters:**
    - `command` (SqlCommand): The command to be sent to the server, with the placeholders.
    - `values` (dynamic[]): The values to be replaced by the placeholders.
