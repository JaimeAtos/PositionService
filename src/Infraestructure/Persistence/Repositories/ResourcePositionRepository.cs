using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Persistence.Context;

namespace Persistence.Repositories;

public class ResourcePositionRepository : IResourcePositionRepository
{
	private readonly PositionDbContext _dbContext;

	public ResourcePositionRepository(PositionDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<Guid> CreateAsync(ResourcePosition entity, CancellationToken cancellationToken = default)
	{
		var task = await Task.Run(async () =>
		{
			var sql =
				"""
                INSERT INTO "ResourcePosition"
                    ("UserCreatorId",
                     "CreationTime", "State",
                     "UserModifiedId",
                     "DateLastModified",
                     "ResourceId",
                     "PositionId",
                     "PercentMathPosition",
                     "IsDefault",
                     "ResourceName")
                VALUES
                    (@UserCreatorId,
                     @CreationTime,
                     @State,
                     @UserModifiedId,
                     @DateLastModified,
                     @ResourceId,
                     @PositionId,
                     @PercentMathPosition,
                     @IsDefault,
                     @ResourceName)
                RETURNING "Id";
                """;
			using var con = _dbContext.CreateConnection();
			var result = await con.ExecuteScalarAsync<Guid>(sql,
				new
				{
					UserCreatorId = Guid.NewGuid(),
					CreationTime = DateTime.UtcNow,
					State = true,
					UserModfiedId = Guid.NewGuid(),
					DateLastModified = DateTime.UtcNow,
					entity.ResourceId,
					entity.PositionId,
					entity.PercentMathPosition,
					entity.IsDefault,
					entity.ResourceName
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
				UPDATE "ResourcePosition"
				SET "State" = false
				WHERE "Id" = @Id
				""";
			using var con = _dbContext.CreateConnection();
			var result = await con.ExecuteAsync(sql, new { Id = id });
			return result > 0;
		}, cancellationToken);
		return task;
	}

	public async Task<IEnumerable<ResourcePosition>> GetAllAsync(object? param = null,
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
				       "ResourceId",
				       "PositionId",
				       "PercentMathPosition",
				       "IsDefault",
				       "ResourceName" FROM "ResourcePosition" WHERE "State" = @IsActive
				                                          ORDER BY "CreationTime" DESC 
				                                          LIMIT 300;
				""";
			using var con = _dbContext.CreateConnection();
			var result = await con.QueryAsync<ResourcePosition>(query, param);
			return result;
		}, cancellationToken);

		return task;
	}

	public async Task<ResourcePosition> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken = default)
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
				       "ResourceId",
				       "PositionId",
				       "PercentMathPosition",
				       "IsDefault",
				       "ResourceName" FROM "ResourcePosition" WHERE "Id" = @ResourcePositionId;
				""";
			using var con = _dbContext.CreateConnection();
			var result = await con.QueryFirstOrDefaultAsync<ResourcePosition>(query, new { ResourcePositionId = id });
			return result;
		}, cancellationToken);

		return task;
	}

	public async Task<bool> UpdateAsync(ResourcePosition entity, Guid id, CancellationToken cancellationToken = default)
	{
		var task = await Task.Run(async () =>
		{
			var sql =
				"""
				UPDATE "ResourcePosition"
				SET "DateLastModified" = @DateLastModified,
					"UserModifiedId" = @UserModifiedId,
					"PercentMathPosition" = @PercentMathPosition,
				    "IsDefault" = @IsDefault,
				    "ResourceName" = @ResourceName
				WHERE "Id" = @Id";
				""";
			using var con = _dbContext.CreateConnection();
			var result = await con.ExecuteAsync(sql, new
			{
				Id = id,
				DateLastModified = DateTime.UtcNow,
				UserModifiedId = Guid.NewGuid(),
				entity.PercentMathPosition,
				entity.IsDefault,
				entity.ResourceName,
			});
			return result > 0;
		}, cancellationToken);
		return task;
	}
}