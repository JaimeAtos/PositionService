using System.Data;
using Npgsql;

namespace Persistence.Context;

public class PositionDbContext
{
	private readonly string _dbConnectionString;

	public PositionDbContext(string dbConnectionString)
	{
		_dbConnectionString = dbConnectionString ??
		                      throw new ($"{nameof(dbConnectionString)} can't be null");
	}

	public IDbConnection CreateConnection()
	{
		return new NpgsqlConnection(_dbConnectionString);
	}
}