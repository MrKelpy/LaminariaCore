using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
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
        /// Sends a command into the connected database. This is a genera command that will return
        /// the query as a Matrix-
        /// </summary>
        /// <param name="query">The MSSQL Statement to be sent to the database to query it</param>
        /// <returns>A Matrix with the results</returns>
        public List<string[]> SendQuery(string query)
        {
            // Gets the connection and executes the query
            SqlConnection conn = this.Connector.Connection;
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            
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
            reader.Close();
            cmd.Dispose();
            return results;
        }
        
        /// <summary>
        /// Reads the content of an SQL script and executes it through a transaction,
        /// separated by "GO" statements.
        /// </summary>
        /// <param name="filepath">The filepath to the script</param>
        /// <returns>The total number of rows affected</returns>
        public int RunSqlScript(string filepath)
        {
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
            string result = "";
            foreach (string s in array) result += s + ", ";
            
            return result.Trim().Trim(',');
        }
    }
}