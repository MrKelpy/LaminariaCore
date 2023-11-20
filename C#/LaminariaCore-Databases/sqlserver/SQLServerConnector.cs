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
    public class SQLServerConnector : IDisposable
    {
        
        /// <summary>
        /// The connection string used to connect to the database.
        /// </summary>
        public string ConnectionString { get; set; }
        
        /// <summary>
        /// The connection object used to access the database
        /// </summary>
        public SqlConnection Connection { get; set; }

        /// <summary>
        /// "Manual" constructor for the SQLServerConnector class. Takes in a connection string
        /// and connects to the database using it.
        /// </summary>
        /// <param name="connectionString">SQL Server Formatted Connection String</param>
        public SQLServerConnector(string connectionString)
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
            string conn = $"Server={server};Database={database};Pooling=False;User={username};Password={password};";
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
            string conn = $"Server={server};Database={database};Pooling=False;Trusted_Connection=True;";
            this.ConnectTo(conn);
        }

        /// <summary>
        /// Disposes of the connection to the database.
        /// </summary>
        public void Dispose() => this.Connection.Dispose();

        /// <summary>
        /// Disconnects from the server closing down any open connections. <br></br>
        /// This is equivalent to calling Dispose().
        /// </summary>
        public void Disconnect() => this.Dispose();
        
        /// <summary>
        /// Reconnects to the server using the last used connection string.
        /// </summary>
        public void Reconnect() => this.ConnectTo(this.ConnectionString);

        /// <summary>
        /// Connects to an SQL Server given a connection string.
        /// </summary>
        /// <param name="connectionString">SQL Server Formatted Connection String</param>
        private void ConnectTo(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.Connection = new SqlConnection(this.ConnectionString);
            this.Connection.Open();
        }
        
    }
}