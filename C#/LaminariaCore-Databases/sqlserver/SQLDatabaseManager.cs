using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LaminariaCore_Databases.enumeration;

namespace LaminariaCore_Databases.sqlserver
{
    /// <summary>
    /// This class is used to manage one or multiple SQL Server databases. This class will be bound
    /// to an SQLServerConnector object and will be used to manage the databases inside of it.
    /// </summary>
    public class SQLDatabaseManager
    {
        
        /// <summary>
        /// The connector object used to connect to the server.
        /// </summary>
        public SQLServerConnector Connector { get; set; }
        
        /// <summary>
        /// General constructor for the SQLDatabaseManager class. Takes in an SQLServerConnector object
        /// and binds it to the class, so that it can interact with the databases inside of it.
        /// </summary>
        /// <param name="connector">The SQLServerConnector object to use</param>
        public SQLDatabaseManager(SQLServerConnector connector)
        {
            this.Connector = connector;
        }
        
        /// <summary>
        /// Switches the currently active database to the one specified.
        /// </summary>
        /// <param name="name">The name of the database to use</param>
        /// <returns>Whether the switch was successful or not</returns>
        public bool UseDatabase(string name)
        {
            try
            {
                this.Connector.Connection.ChangeDatabase(name);
                return true;
            }
            catch (InvalidOperationException) { return false; }
            catch (SqlException) { return false; }
        }

        /// <summary>
        /// Exports the specified database to the specified filepath.
        /// </summary>
        /// <param name="name">The name of the database to backup</param>
        /// <param name="filepath">The path to save the backup to</param>
        public void ExportDatabase(string name, string filepath)
        {
            // Saves the current database and switches to master
            string currentDatabase = this.Connector.Connection.Database;
            this.UseDatabase("master");
            
            // Ensures that the file path's extension is .bak
            if (Path.GetExtension(filepath) != ".bak") filepath += ".bak";

            // Sends the backup command and switches back to the original database
            this.SendNonQuery($"BACKUP DATABASE [{name}] TO  DISK = '{filepath}'");
            this.UseDatabase(currentDatabase);
        }
        
        /// <summary>
        /// Imports the specified database from the specified filepath.
        /// </summary>
        /// <param name="name">The name of the database to restore from</param>
        /// <param name="filepath">The path to restore the backup from</param>
        /// <returns>The number of rows affected</returns>
        public int ImportDatabase(string name, string filepath)
        {
            // The file must be a .bak file
            if (Path.GetExtension(filepath) != ".bak") 
                throw new FormatException("The file path must be a .bak file.");

            // Sends the restore command and returns the number of rows affected
            return this.SendNonQuery($"DROP DATABASE IF EXISTS {name}; RESTORE DATABASE [{name}] FROM DISK = '{filepath}'");
        }
        
        /// <summary>
        /// Performs a composite INSERT INTO query into the specified table, restricting the addition to specified
        /// fields and values.
        /// </summary>
        /// <param name="table">The table to add the content into</param>
        /// <param name="fields">The fields restricting the values to be added into the table</param>
        /// <param name="values">The values matching the restriction fields</param>
        /// <returns>Whether or not changes happened in the database</returns>
        public bool InsertInto(string table, string[] fields, params dynamic[] values)
        {
            if (fields.Length != 0 && fields.Length != values.Length) throw new ArgumentException("The number of fields and values for the query must be the same.");
            
            // Creates the query 
            string fieldsEntry = fields.Length != 0 ? $" {this.ArrayToQueryString(fields)}" : string.Empty;
            
            string query = $"INSERT INTO {table}" + fieldsEntry +
                           $" VALUES {this.CreatePlaceholdersArrayString(values.Length)}";
            
            // Adds the values into the command
            using SqlCommand command = new SqlCommand(query, this.Connector.Connection);
            this.AddValuesIntoCommand(command, "p", values);
            
            return command.ExecuteNonQuery() > 0;
        }
        
        /// <summary>
        /// Performs a simple INSERT INTO query into the specified table, with the specified values.
        /// <br></br>
        /// This method assumes that you are inserting into all of the columns in the table. For a more specialised
        /// insert use the InsertInto(string, string[], dynamic[]) method.
        /// </summary>
        /// <param name="table">The table to add the values into</param>
        /// <param name="values">The values to add into the table</param>
        /// <returns>Whether or not changes happened in the database</returns>
        public bool InsertInto(string table, params dynamic[] values) => this.InsertInto(table, Array.Empty<string>(), values);

        /// <summary>
        /// Performs DELETE FROM query into the specified table, with the specified condition.
        /// </summary>
        /// <param name="table">The table to delete the data from</param>
        /// <param name="condition">A string specifying the condition for the entries to be deleted</param>
        /// <returns>The number of rows affected</returns>
        public int DeleteFrom(string table, string condition)
        {
            // Builds the query
            string query = $"DELETE FROM {table} WHERE {condition}";
            
            // Adds the values into the command
            using SqlCommand command = new SqlCommand(query, this.Connector.Connection);
            
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// Updates all the fields in a set table, with the specified value, based on a condition.
        /// </summary>
        /// <param name="table">The table to update the field of</param>
        /// <param name="fieldToUpdate">The field to be updated</param>
        /// <param name="value">The value to add to the field</param>
        /// <param name="condition">An optional search condition to narrow down field entries</param>
        /// <returns>The number of rows affected</returns>
        public int Update(string table, string fieldToUpdate, dynamic value, string condition)
        {
            // Builds the query based on the parameters, adding a condition if specified
            string query = $"UPDATE {table} SET {fieldToUpdate} = @p1";
            
            if (condition != null) query += " WHERE " + condition;
            
            using SqlCommand command = new SqlCommand(query, this.Connector.Connection);
            command.Parameters.AddWithValue("@p0", fieldToUpdate);
            command.Parameters.AddWithValue("@p1", value);
            
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// Updates all the fields in a set table, with the specified value.
        /// </summary>
        /// <param name="table">The table to update the field of</param>
        /// <param name="fieldToUpdate">The field to be updated</param>
        /// <param name="value">The value to add to the field</param>
        /// <returns>The number of rows affected</returns>
        public int Update(string table, string fieldToUpdate, dynamic value) =>
            this.Update(table, fieldToUpdate, value, null);

        /// <summary>
        /// Performs a SELECT query into the specified table, with the specified fields and condition.
        /// </summary>
        /// <param name="fields">The fields to select from the table</param>
        /// <param name="table">The table to select the fields from</param>
        /// <param name="condition">The condition to narrow down the results</param>
        /// <param name="options">The SQL Query options to modify the result</param>
        /// <returns>A matrix containing the results</returns>
        public List<string[]> Select(string[] fields, string table, string condition, SQLQueryOptions options = SQLQueryOptions.NoHeaders)
        {
            // Builds the query based on the parameters, adding a condition if specified
            
            string query = $"SELECT {this.ArrayToQueryString(fields).Trim('(').Trim(')')} FROM {table}";
            
            if (condition != null) query += " WHERE " + condition;

            // Gets the columns for the table removing the ones that are not in the query
            string[] columns = this.GetColumnsForTable(table);
            if (!fields[0].Equals("*")) columns = columns.Where(fields.Contains).ToArray();
            
            // Adds the values into the placeholders
            using SqlCommand command = new SqlCommand(query, this.Connector.Connection);
            this.AddValuesIntoCommand(command, "f", fields);
            
            // Sends the query and inserts the columns at the start of the results
            List<string[]> results = this.SendQuery(command);
            if (options == SQLQueryOptions.IncludeHeaders) results.Insert(0, columns);
            
            return results;
        }
        
        /// <summary>
        /// Performs a SELECT query into the specified table, with the specified fields.
        /// </summary>
        /// <param name="fields">The fields to select from the table</param>
        /// <param name="table">The table to select the fields from</param>
        /// <param name="options">The SQL Query options to modify the result</param>
        /// <returns>A matrix containing the results</returns>
        public List<string[]> Select(string[] fields, string table, SQLQueryOptions options = SQLQueryOptions.NoHeaders) =>
            this.Select(fields, table, null, options: options);
        
        /// <summary>
        /// Performs a 'SELECT *' query into the specified table, with the condition.
        /// </summary>
        /// <param name="table">The table to select the fields from</param>
        /// <param name="condition">The condition to narrow down the results</param>
        /// <param name="options">The SQL Query options to modify the result</param>
        /// <returns>A matrix containing the results</returns>
        public List<string[]> Select(string table, string condition, SQLQueryOptions options = SQLQueryOptions.NoHeaders) =>
            this.Select(new [] {"*"}, table, condition, options: options);

        
        /// <summary>
        /// Performs a 'SELECT *' query into the specified table. This is equivalent to doing 'SELECT * FROM [Table]'
        /// </summary>
        /// <param name="table">The table to select the fields from</param>
        /// <param name="options">The SQL Query options to modify the result</param>
        /// <returns>A matrix containing the results</returns>
        public List<string[]> Select(string table, SQLQueryOptions options = SQLQueryOptions.NoHeaders) =>
            this.Select(new [] {"*"}, table, null, options: options);
        
        /// <summary>
        /// Creates a view in the database with the specified name, fields, values and condition.
        /// </summary>
        /// <remarks style="color:red">
        /// This method is not safe and should be used with caution. SQL Injection is possible when using this method,
        /// so, sanitise your inputs before using it.
        /// </remarks>
        /// <param name="viewName">The name of the view to be created</param>
        /// <param name="fields">The fields to be included in the view</param>
        /// <param name="tables">The tables to get the fields from</param>
        /// <param name="condition">The conditional restrictions for the inclusion of values</param>
        /// <returns>Whether or not the view was created</returns>
        public bool CreateView(string viewName, string[] fields, string[] tables, string condition)
        {
            if (fields.Length != tables.Length && (fields.Length != 1 && fields[0].Equals("*"))) 
                throw new ArgumentException("The number of fields and tables for the query must be the same.");
            
            string query = $"CREATE VIEW {viewName} as (SELECT {this.ArrayToQueryString(fields).Trim('(').Trim(')')} " +
                           $"FROM {this.ArrayToQueryString(tables).Trim('(').Trim(')')})";

            // Adds the condition if specified
            if (condition != null) query += " WHERE " + condition;

            this.SendNonQuery(query);
            return true;
        }
        
        /// <summary>
        /// Checks whether or not a database with the specified name exists.
        /// </summary>
        /// <param name="databaseName">The name of the database to check for</param>
        /// <returns>True if it exists, false otherwise.</returns>
        public bool DatabaseExists(string databaseName)
        {
            string query = $"SELECT * FROM sys.databases WHERE name = '{databaseName}'";
            return this.SendQuery(new SqlCommand(query, this.Connector.Connection)).Count > 0;
        }
        
        /// <summary>
        /// Creates a view in the database with the specified name, fields and values.
        /// </summary>
        /// <param name="viewName">The name of the view to be created</param>
        /// <param name="fields">The fields to be included in the view</param>
        /// <param name="tables">The tables to get the fields from</param>
        /// <returns>Whether or not the view was created</returns>
        public bool CreateView(string viewName, string[] fields, string[] tables) => 
            this.CreateView(viewName, fields, tables, null);

        /// <summary>
        /// Sends a command into the connected database. This is a general command that will return
        /// the query as a Matrix.
        /// </summary>
        /// <param name="command">The MSSQL command to be sent to the database to query it</param>
        /// <returns>A matrix with the results</returns>
        public List<string[]> SendQuery(SqlCommand command)
        {
            // Gets the connection and executes the query
            using SqlDataReader reader = command.ExecuteReader();
            
            // Creates the matrix to store the results
            List<string[]> results = new List<string[]>();

            // Gets the values from the reader and stores them in an array, then added to a matrix.
            while (reader.Read())
            {
                string[] rowValues = new string[reader.FieldCount];
                
                for (int i = 0; i < reader.FieldCount; i++)
                    rowValues[i] = reader[i].ToString();
                
                results.Add(rowValues);
            }

            // Cleans up the connection and returns the results
            return results;
        }

        /// <summary>
        /// Sends a query to the database based on a query string.
        /// </summary>
        /// <param name="query">The query to be send into the database</param>
        /// <returns>A matrix with the results</returns>
        public List<string[]> SendQuery(string query)
        {
            SqlCommand command = new SqlCommand(query, this.Connector.Connection);
            return this.SendQuery(command);
        }
        
        /// <summary>
        /// Sends a non-query (no expected results) instruction to the database.
        /// </summary>
        /// <param name="query">The query to send</param>
        /// <returns>The number of rows affected</returns>
        public int SendNonQuery(string query)
        {
            using SqlCommand cmd = new SqlCommand(query, this.Connector.Connection);
            return cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Reads the content of an SQL script and executes it through a transaction,
        /// separated by "GO" statements.
        /// </summary>
        /// <param name="filepath">The filepath to the script</param>
        /// <returns>The total number of rows affected</returns>
        public int RunSqlScript(string filepath)
        {
            // Saves the current database and switches to master
            this.UseDatabase("master");
            
            // Reads the file and splits it into GO batches
            int rowsAffected = 0;
            string script = File.ReadAllText(filepath);
            string[] batches = Regex.Split(script, @"(?i-msnx:\b(?<!-{2,}.*)go[^a-zA-Z])", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            // Runs each batch separately as a non-query
            foreach (string statement in batches)
            {
                if (statement.Trim().Length == 0) continue;
                
                using SqlCommand command = new SqlCommand(statement.Trim(), this.Connector.Connection);
                rowsAffected += command.ExecuteNonQuery();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Returns an array containing the names of all the tables in the database.
        /// </summary>
        /// <param name="tableName">The name of the table to get the columns for</param>
        /// <returns>The column names for the table</returns>
        public string[] GetColumnsForTable(string tableName)
        {
            string query = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}';";
            
            using SqlCommand cmd = new SqlCommand(query, this.Connector.Connection);
            using SqlDataReader reader = cmd.ExecuteReader();
            
            // Creates the list to store the results that will then be converted into an array
            List<string> results = new List<string>();

            // Gets the values from the reader and stores them in an array, then added to a matrix.
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    results.Add(reader[i].ToString());
            }

            return results.ToArray();
        }


        /// <summary>
        /// Converts an array of strings into a query string, with each element separated by a comma.
        /// </summary>
        /// <param name="array">The array to be converted</param>
        /// <returns>The query string to be used to specify the query objects</returns>
        public string ArrayToQueryString(params string[] array)
        {
            // If the array is empty, returns an empty string
            if (array.Length == 0) return string.Empty;
            
            // Adds each element of the array into the query string
            StringBuilder result = new StringBuilder();
            foreach (string s in array) result.Append(s + ", ");
            
            return "(" + result.ToString().Trim().Trim(',') + ")";
        }

        /// <summary>
        /// Creates all of the placeholders to for a certain array about to be used in a query.
        /// <br></br>
        /// The placeholders are in the format of @p0, @p1, @p2, etc with the aim of being replaced
        /// by the actual typed values in the array.
        /// </summary>
        /// <param name="arraySize">The size of the array to convert into placeholders</param>
        /// <param name="parameterName">The name of the parameter to consider</param>
        /// <returns>The string containing the placeholders for the array</returns>
        public string CreatePlaceholdersArrayString(int arraySize, string parameterName = "p")
        {
            // If the array is empty, returns an empty string
            if (arraySize == 0) return string.Empty;
            
            StringBuilder result = new StringBuilder();

            // Adds a placeholder equal to the current size index of the array
            for (int i = 0; i < arraySize; i++)
                result.Append($"@{parameterName}" + i + ", ");

            return "(" + result.ToString().Trim().Trim(',') + ")";
        }

        /// <summary>
        /// Adds the values into the command as parameters, with the placeholders being @p0, @p1, @p2, etc.
        /// <br></br>
        /// This method is meant to be used alongside the CreatePlaceholdersArrayString(int) method, to insert
        /// typed values into the query.
        /// </summary>
        /// <param name="command">The command to be sent to the server, with the placeholders up</param>
        /// <param name="parameterName">The name of the parameter to change</param>
        /// <param name="values">The values to be replaced by the placeholders</param>
        public void AddValuesIntoCommand(SqlCommand command, string parameterName = "p", params dynamic[] values)
        {
            // Adds the typed values into the command
            for (int i = 0; i < values.Length; i++)
            {
                string placeholder = $"@{parameterName}" + i;
                command.Parameters.AddWithValue(placeholder, values[i]);
            }
        }
        
        
        /// <summary>
        /// Checks if the fields array is only a "*", in which case it should return the "*" to use the
        /// SQL * identifier. Otherwise, it should return the placeholders for the array.
        /// </summary>
        /// <param name="fields">The fields to be processed</param>
        /// <param name="parameterName">The parameter name to create the placeholder with</param>
        /// <returns>Either * or a placeholder string.</returns>
        private string ProcessFields(string[] fields, string parameterName = "f") => 
            fields.Length == 1 && fields[0].Equals("*") ? "*" : this.CreatePlaceholdersArrayString(fields.Length, parameterName);
    }
}