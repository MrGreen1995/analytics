using System.Data;
using Microsoft.Data.SqlClient;
using Npgsql;
using UzEx.Analytics.Application.Abstractions.Data;

namespace UzEx.Analytics.Infrastructure.Data;

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _postgresConnectionString;
    private readonly string _mssqlConnectionString;
    private readonly string _oracleConnectionString;

    public SqlConnectionFactory(string postgresConnectionString, string mssqlConnectionString, string oracleConnectionString)
    {
        _postgresConnectionString = postgresConnectionString;
        _mssqlConnectionString = mssqlConnectionString;
        _oracleConnectionString = oracleConnectionString;
    }

    public IDbConnection CreatePostgresConnection()
    {
        var connection = new NpgsqlConnection(_postgresConnectionString);
        connection.Open();
        
        return connection;
    }

    public IDbConnection CreateMssqlConnection()
    {
        var connection = new SqlConnection(_mssqlConnectionString);
        connection.Open();
        
        return connection;
    }

    public IDbConnection CreateOracleConnection()
    {
        // TODO: Oracle connection create
        throw new NotImplementedException();
    }
}