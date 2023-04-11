using System.Data;
using Npgsql;

namespace Persistence.Context;
public class PositionDbContext
{
    private readonly string _dbConnectionString;

    public PositionDbContext(string dbConnectionString)
    {
        if(dbConnectionString is null)
            throw new ArgumentNullException($"{nameof(dbConnectionString)} can't be null");
        _dbConnectionString=dbConnectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_dbConnectionString);
    }
}