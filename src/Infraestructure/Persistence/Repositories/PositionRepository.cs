using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Persistence.Context;

namespace Persistence.Repositories;

public class PositionRepository : IPositionRepository
{
	private readonly PositionDbContext _dbContext;

	public PositionRepository(PositionDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<Guid> CreateAsync(Position entity, CancellationToken cancellationToken = default)
	{
		var task = await Task.Run(async () =>
		{
			var sql =
				"""
				INSERT INTO "Position"
					("UserCreatorId",
					 "CreationTime",
					 "UserModifiedId",
					 "DateLastModified",
					 "State",
					 "Description",
					 "ClientId",
					 "ClientDescription",
					 "PositionLevel")
				VALUES
					(@UserCreatorId,
					 @CreationTIme,
					 @UserModifiedId,
					 @DateLastModified,
					 @State,
					 @Description,
					 @ClientId,
					 @ClientDescription,
					 @PositionLevel)
				RETURNING "Id";
				""";
			using var con = _dbContext.CreateConnection();
			var result = await con.ExecuteScalarAsync<Guid>(sql,
				new
				{
					UserCreatorId = Guid.NewGuid(),
					CreationTime = DateTime.UtcNow,
					UserModifiedId = Guid.NewGuid(),
					DateLastModified = DateTime.UtcNow,
					State = true,
					entity.Description,
					entity.ClientId,
					entity.ClientDescription,
					entity.PositionLevel
				});
			return result;
		}, cancellationToken);
		return task;
	}

	public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var task = await Task.Run(async () =>
		{
			var sql =
				"""
				UPDATE "Position"
				SET "State" = false,
				    "UserModifiedId" = @UserModifiedId,
				    "DateLastModified" = @DateLastModified
				WHERE "Id" = @Id
				""";
			using var con = _dbContext.CreateConnection();
			var result = await con.ExecuteAsync(sql, new
			{
				Id = id,
				UserModifiedId = Guid.NewGuid(),
				DateLastModified = DateTime.UtcNow
			});
			return result > 0;
		}, cancellationToken);
		return task;
	}

	/*TODO: designar a alguien que se encargue de realizar la paginacion dentro de dapper*/
	/*Para usar FETCH es necesario usar cursores*/
	public async Task<IEnumerable<Position>> GetAllAsync(object? param = null,
		CancellationToken cancellationToken = default)
	{
		var task = await Task.Run(async () =>
		{
			var query =
				"""
				SELECT "UserCreatorId",
				       "CreationTime",
				       "State",
				       "UserModifiedId",
				       "DateLastModified",
				       "Description",
				       "ClientId",
				       "ClientDescription",
				       "PositionLevel"
				FROM "Position" WHERE "State" = @IsActive
				                ORDER BY "CreationTime" DESC
				                LIMIT 300 OFFSET 0;
				""";
			using var con = _dbContext.CreateConnection();
			var result = await con.QueryAsync<Position>(query, param);
			return result;
		}, cancellationToken);

		return task;
	}

	public async Task<Position> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var task = await Task.Run(async () =>
		{
			var query =
				"""
				SELECT "UserCreatorId",
				       "CreationTime",
				       "State",
				       "UserCreatorId",
				       "DateLastModified",
				       "Description",
				       "ClientId",
				       "ClientDescription",
				       "PositionLevel"
				FROM "Position" WHERE "Id" = @PositionId;
				""";
			using var con = _dbContext.CreateConnection();
			var result = await con.QueryFirstOrDefaultAsync<Position>(query, new { @PositionId = id });
			return result;
		}, cancellationToken);

		return task;
	}

	public async Task<bool> UpdateAsync(Position entity, Guid id, CancellationToken cancellationToken = default)
	{
		var task = await Task.Run(async () =>
		{
			var sql =
				"""
				UPDATE "Position"
				SET "UserModifiedId" = @UserModifiedId,
				    "DateLastModified" = @DateLastModified,
				    "Description" = @Description,
				    "ClientId" = @ClientId,
				    "ClientDescription" = @ClientDescription,
				    "PositionLevel" = @PositionLevel
				WHERE "Id" = @Id
				""";
			using var con = _dbContext.CreateConnection();
			var result = await con.ExecuteAsync(sql, new
			{
				Id = id,
				UserModifiedId = Guid.NewGuid(),
				DateLastModified = DateTime.UtcNow,
				entity.Description,
				entity.ClientId,
				entity.ClientDescription,
				entity.PositionLevel
			});
			return result > 0;
		}, cancellationToken);
		return task;
	}
}
