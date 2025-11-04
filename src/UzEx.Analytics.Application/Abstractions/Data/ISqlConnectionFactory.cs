using System.Data;

namespace UzEx.Analytics.Application.Abstractions.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreatePostgresConnection();
    IDbConnection CreateMssqlConnection();
    IDbConnection CreateOracleConnection();
}