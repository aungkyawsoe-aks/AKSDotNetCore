using Microsoft.Data.SqlClient;
using System.Data;

internal class AdoDotNetService
{
    private SqlConnectionStringBuilder sqlConnectionStringBuilder;

    public AdoDotNetService(SqlConnectionStringBuilder sqlConnectionStringBuilder)
    {
        this.sqlConnectionStringBuilder = sqlConnectionStringBuilder;
    }

    internal int Execute(string query, System.Data.SqlClient.SqlParameter[] sqlParamters)
    {
        throw new NotImplementedException();
    }

    internal DataTable Query(string query, System.Data.SqlClient.SqlParameter sqlParameters)
    {
        throw new NotImplementedException();
    }
}