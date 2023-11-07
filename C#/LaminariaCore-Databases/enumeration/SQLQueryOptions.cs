namespace LaminariaCore_Databases.enumeration
{
    /// <summary>
    /// This class contains a the query options to be used in the SQLDatabaseManager, to modify
    /// the results received from the queries.
    /// </summary>
    public enum SQLQueryOptions
    {
        /// <summary>
        /// Using this option enables the inclusion of an initial headers array in the query results
        /// </summary>
        IncludeHeaders,
        
        /// <summary>
        /// Using this option prevents the query results from including the initial headers array
        /// </summary>
        NoHeaders
    }
}