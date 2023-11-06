using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

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
        private SQLServerConnector Connector { get; set; }
        
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
            // Switches to master to perform the restore
            this.UseDatabase("master");
            
            // The file must be a .bak file
            if (Path.GetExtension(filepath) != ".bak") 
                throw new FormatException("The file path must be a .bak file.");

            // Sends the restore command and returns the number of rows affected
            return this.SendNonQuery($"RESTORE DATABASE [{name}] FROM DISK = '{filepath}'");
        }


        /// <summary>
        /// Performs a composite INSERT INTO query into the specified table, restricting the addition to specified
        /// fields and values.
        /// </summary>
        /// <param name="table">The table to add the content into</param>
        /// <param name="fields">The fields restricting the values to be added into the table</param>
        /// <param name="values">The values matching the restriction fields</param>
        /// <returns>Whether or not changes happened in the database</returns>
        public bool InsertIntoRestricted(string table, string[] fields, params dynamic[] values)
        {
            if (fields.Length != values.Length) throw new ArgumentException("The number of fields and values for the query must be the same.");
            
            string query = "INSERT INTO " + table + ' ' + this.ArrayToQueryString(fields) + " VALUES" + this.CreatePlaceholdersArrayString(values.Length);
            using SqlCommand command = new SqlCommand(query, this.Connector.Connection);
            this.AddValuesIntoCommand(command, values);
            
            return command.ExecuteNonQuery() > 0;
        }
        
        /// <summary>
        /// Performs a simple INSERT INTO query into the specified table, with the specified values.
        /// <br></br>
        /// This method assumes that you are inserting into all of the columns in the table. For a more specialised
        /// insert use the SendNonQuery(string) method.
        /// </summary>
        /// <param name="table">The table to add the values into</param>
        /// <param name="values">The values to add into the table</param>
        /// <returns>Whether or not changes happened in the database</returns>
        public bool InsertInto(string table, params dynamic[] values)
        {
            string query = "INSERT INTO " + table + " VALUES " + this.CreatePlaceholdersArrayString(values.Length);
            using SqlCommand command = new SqlCommand(query, this.Connector.Connection);
            this.AddValuesIntoCommand(command, values);
            
            return command.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Sends a command into the connected database. This is a genera command that will return
        /// the query as a Matrix-
        /// </summary>
        /// <param name="query">The MSSQL Statement to be sent to the database to query it</param>
        /// <returns>A Matrix with the results</returns>
        public List<string[]> SendQuery(string query)
        {
            // Gets the connection and executes the query
            using SqlCommand cmd = new SqlCommand(query, this.Connector.Connection);
            using SqlDataReader reader = cmd.ExecuteReader();
            
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
        /// <returns>The string containing the placeholders for the array</returns>
        public string CreatePlaceholdersArrayString(int arraySize)
        {
            // If the array is empty, returns an empty string
            if (arraySize == 0) return string.Empty;
            
            StringBuilder result = new StringBuilder();

            // Adds a placeholder equal to the current size index of the array
            for (int i = 0; i < arraySize; i++)
                result.Append("@p" + i + ", ");

            return "(" + result.ToString().Trim().Trim(',') + ")";
        }

        /// <summary>
        /// Adds the values into the command as parameters, with the placeholders being @p0, @p1, @p2, etc.
        /// <br></br>
        /// This method is meant to be used alongside the CreatePlaceholdersArrayString(int) method, to insert
        /// typed values into the query.
        /// </summary>
        /// <param name="command">The command to be sent to the server, with the placeholders up</param>
        /// <param name="values">The values to be replaced by the placeholders</param>
        public void AddValuesIntoCommand(SqlCommand command, params dynamic[] values)
        {
            // Adds the typed values into the command
            for (int i = 0; i < values.Length; i++)
            {
                string placeholder = "@p" + i;
                command.Parameters.AddWithValue(placeholder, values[i]);
            }
        }
    }
}