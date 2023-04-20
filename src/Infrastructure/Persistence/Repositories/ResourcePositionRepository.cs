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
                     "CreationTime",
                     "State",
                     "UserModifierId",
                     "DateLastModify",
                     "ResourceId",
                     "PositionId",
                     "PercentMatchPosition",
                     "IsDefault",
                     "ResourceName",
                     "RomaId")
                VALUES
                    (@UserCreatorId,
                     @CreationTime,
                     @State,
                     @UserModifierId,
                     @DateLastModify,
                     @ResourceId,
                     @PositionId,
                     @PercentMatchPosition,
                     @IsDefault,
                     @ResourceName,
                     @RomaId)
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
					DateLastModify = DateTime.UtcNow,
					entity.ResourceId,
					entity.PositionId,
					PercentMathPosition = entity.PercentMatchPosition,
					entity.IsDefault,
					entity.ResourceName,
					entity.RomaId
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
				SET "State" = false,
				    "UserModifierId" = @UserModifierId,
				    "DateLastModify" = @DateLastModify
				WHERE "Id" = @Id
				""";
			using var con = _dbContext.CreateConnection();
			var result = await con.ExecuteAsync(sql, new
			{
				Id = id,
				UserModifierId = Guid.NewGuid(),
				DateLastModify = DateTime.UtcNow
			});
			return result > 0;
		}, cancellationToken);
		return task;
	}

	public async Task<IEnumerable<ResourcePosition>> GetAllAsync(int page, int offset, Dictionary<string, object> param,
		CancellationToken cancellationToken = default)
	{
		var task = await Task.Run(async () =>
		{
			var query =
				"""
				SELECT "Id",
				       "UserCreatorId",
				       "CreationTime",
				       "State",
				       "UserModifierId",
				       "DateLastModify",
				       "ResourceId",
				       "PositionId",
				       "PercentMatchPosition",
				       "IsDefault",
				       "RomaId",
				       "ResourceName" FROM "ResourcePosition" /**where**/
				                      OFFSET @Offset
				                          FETCH NEXT @PageSize ROWS ONLY;
				""";
			var sb = new SqlBuilder();
			var template = sb.AddTemplate(query);

			foreach (var key in param.Select(fields => fields.Key))
			{
				sb.Where($$"""
								{{key}} = @{{key}}
							""");
			}

			param.Add("Offset", (page < 1 ? 0 : page - 1) * offset);
			param.Add("Page", offset);
			
			var parameters = new DynamicParameters(param);
			
			using var con = _dbContext.CreateConnection();
			var result = await con.QueryAsync<ResourcePosition>(template.RawSql, parameters);
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
				SELECT "Id",
				       "UserCreatorId",
				       "CreationTime",
				       "State",
				       "UserModifierId",
				       "DateLastModify",
				       "ResourceId",
				       "PositionId",
				       "PercentMatchPosition",
				       "IsDefault",
				       "RomaId",
				       "ResourceName" FROM "ResourcePosition" WHERE "Id" = @Id;
				""";
			using var con = _dbContext.CreateConnection();
			var result = await con.QueryFirstOrDefaultAsync<ResourcePosition>(query,
				new { Id = id });
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
				SET "DateLastModify" = @DateLastModify,
					"UserModifierId" = @UserModifierId,
					"PercentMatchPosition" = @PercentMatchPosition,
				    "IsDefault" = @IsDefault,
				    "ResourceName" = @ResourceName,
				    "RomaId" = @RomaId
				WHERE "Id" = @Id;
				""";
			using var con = _dbContext.CreateConnection();
			var result = await con.ExecuteAsync(sql, new
			{
				Id = id,
				DateLastModify = DateTime.UtcNow,
				UserModifierId = Guid.NewGuid(),
				PercentMathPosition = entity.PercentMatchPosition,
				entity.IsDefault,
				entity.ResourceName,
				entity.RomaId
			});
			return result > 0;
		}, cancellationToken);
		return task;
	}
}
