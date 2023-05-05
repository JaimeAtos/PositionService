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
					 "UserModifierId",
					 "DateLastModify",
					 "State",
					 "Description",
					 "CatalogLevelDescription",
					 "CatalogLevelId",
					 "ClientId",
					 "ClientDescription")
				VALUES
					(@UserCreatorId,
					 @CreationTIme,
					 @UserModifiedId,
					 @DateLastModified,
					 @State,
					 @Description,
					 @CatalogLevelDescription,
					 @CatalogLevelId,
					 @ClientId,
					 @ClientDescription)
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
					entity.CatalogLevelDescription,
					entity.CatalogLevelId,
					entity.ClientId,
					entity.ClientDescription
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
				    "UserModifierId" = @UserModifiedId,
				    "DateLastModify" = @DateLastModified
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
	
	public async Task<IEnumerable<Position>> GetAllAsync(Dictionary<string, object> param,
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
				       "Description",
				       "CatalogLevelDescription",
				       "CatalogLevelId",
				       "ClientId",
				       "ClientDescription"
				FROM "Position" /**where**/
				;
				""";

			var sb = new SqlBuilder();
			var template = sb.AddTemplate(query);

			foreach (var key in param.Select(fields => fields.Key))
			{
				sb.Where($$"""
									"{{key}}" = @{{key}}
								""");
			}

			var parameters = new DynamicParameters(param);

			using var con = _dbContext.CreateConnection();
			var result = await con.QueryAsync<Position>(template.RawSql, parameters);

			return result;
		}, cancellationToken);

		return task;
	}

	public async Task<IEnumerable<Position>> GetAllAsync(int page, int offset,
		Dictionary<string, object> param,
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
				       "Description",
				       "CatalogLevelDescription",
				       "CatalogLevelId",
				       "ClientId",
				       "ClientDescription"
				FROM "Position" /**where**/
				OFFSET @Offset
				FETCH NEXT @PageSize ROWS ONLY;
				""";

			var sb = new SqlBuilder();
			var template = sb.AddTemplate(query);

			foreach (var key in param.Select(fields => fields.Key))
			{
				sb.Where($$"""
									"{{key}}" = @{{key}}
								""");
			}

			param.Add("Offset", (page < 1 ? 0 : page - 1) * offset);
			param.Add("PageSize", offset);

			var parameters = new DynamicParameters(param);

			using var con = _dbContext.CreateConnection();
			var result = await con.QueryAsync<Position>(template.RawSql, parameters);

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
				SELECT "Id",
				       "UserCreatorId",
				       "CreationTime",
				       "State",
				       "UserModifierId",
				       "DateLastModify",
				       "Description",
				       "CatalogLevelDescription",
				       "CatalogLevelId",
				       "ClientId",
				       "ClientDescription"
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
				SET "UserModifierId" = @UserModifiedId,
				    "DateLastModify" = @DateLastModified,
				    "Description" = @Description,
				    "CatalogLevelDescription" = @CatalogLevelDescription,
				    "CatalogLevelId" = @CatalogLevelId,
				    "ClientId" = @ClientId,
				    "ClientDescription" = @ClientDescription
				WHERE "Id" = @Id
				""";
			using var con = _dbContext.CreateConnection();
			var result = await con.ExecuteAsync(sql, new
			{
				Id = id,
				UserModifiedId = Guid.NewGuid(),
				DateLastModified = DateTime.UtcNow,
				entity.Description,
				entity.CatalogLevelDescription,
				entity.CatalogLevelId,
				entity.ClientId,
				entity.ClientDescription
			});
			return result > 0;
		}, cancellationToken);
		return task;
	}
}
