# SQLServerConnector Class Documentation

## Overview
The `SQLServerConnector` class is used to connect to a SQL Server database. It handles the connection and disconnection of the database and serves as the parent for other classes.

**Note**: It is important to either instantiate this class once across the program or closely monitor the closing of the connections.

## Constructors

### `SQLServerConnector(string connectionString)`
- Initializes a new instance of the `SQLServerConnector` class with a manual constructor.
- Takes in a SQL Server formatted connection string and connects to the database using it.
- **Parameters:**
    - `connectionString` (string): SQL Server Formatted Connection String.

### `SQLServerConnector(string server, string database, string username, string password)`
- Initializes a new instance of the `SQLServerConnector` class with an automatic constructor.
- Takes in server location (IP), database name, username, and password to create a connection to the database.
- **Parameters:**
    - `server` (string): The location of the server to connect to (IP)(ServerType).
    - `database` (string): The database name to use.
    - `username` (string): The username to authenticate as.
    - `password` (string): The password to use to connect.

### `SQLServerConnector(string server, string database)`
- Initializes a new instance of the `SQLServerConnector` class with an automatic constructor.
- Takes in server location (IP) and database name and assumes the connection should be done through Windows Credentials.
- **Parameters:**
    - `server` (string): The location of the server to connect to (IP)(ServerType).
    - `database` (string): The database name to use.

## Methods

### `Dispose()`
- Disposes of the connection to the database.
- This method should be used to release the resources when you're done with the database connection.

## Private Methods

### `ConnectTo(string connectionString)`
- Connects to an SQL Server using a given connection string.
- Used internally to establish the database connection.
- **Parameters:**
    - `connectionString` (string): SQL Server Formatted Connection String.
