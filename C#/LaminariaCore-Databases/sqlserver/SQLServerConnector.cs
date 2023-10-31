using System;
using System.Data.SqlClient;

namespace LaminariaCore_Databases.sqlserver
{
    /// <summary>
    /// This class is used to connect to a SQL Server database. Handles the connection
    /// and disconnection of the database, and serves as the parent for other classes.
    /// 
    /// <span style="color: red;">
    /// Keep in mind that this class should either be instantiated once across the program
    /// or the closing of the connections should be closely monitored.
    /// </span>
    /// 
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class SQLServerConnector
    {
        
        /// <summary>
        /// The connection string used to connect to the database.
        /// </summary>
        public SqlConnection Connection { get; set; }

        /// <summary>
        /// "Manual" constructor for the SQLServerConnector class. Takes in a connection string
        /// and connects to the database using it.
        /// </summary>
        /// <param name="connectionString">SQL Server Formatted Connection String</param>
        public SQLServerConnector(String connectionString)
        {
            this.ConnectTo(connectionString);
        }

        /// <summary>
        /// "Automatic" constructor for the SQLServerConnector class. Takes in a few arguments
        /// in order to create the connection to the database.
        /// </summary>
        /// <param name="server">The location of the server to connect to (IP)(ServerType)</param>
        /// <param name="database">The database name to use</param>
        /// <param name="username">The username to authenticate as</param>
        /// <param name="password">The password to use to connect</param>
        public SQLServerConnector(string server, string database, string username, string password)
        {
            String conn = $"Server={server};Database={database};User={username};Password={password};";
            this.ConnectTo(conn);
        }

        /// <summary>
        /// "Automatic" constructor for the SQLServerConnector class. Takes in a few arguments
        /// and assumes the connection should be done through Windows Credentials.
        /// </summary>
        /// <param name="server">The location of the server to connect to (IP)(ServerType)</param>
        /// <param name="database">The database name to use</param>
        public SQLServerConnector(string server, string database)
        {
            String conn = $"Server={server};Database={database};Trusted_Connection=True;";
            this.ConnectTo(conn);
        }

        /// <summary>
        /// Disconnects from the SQL Server.
        /// </summary>
        public void CloseConnection() => this.Connection.Close();

        /// <summary>
        /// Connects to an SQL Server given a connection string.
        /// </summary>
        /// <param name="connectionString">SQL Server Formatted Connection String</param>
        private void ConnectTo(string connectionString)
        {
            this.Connection = new SqlConnection(connectionString);
            this.Connection.Open();
        }
    }
}