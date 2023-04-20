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

	public async Task<IEnumerable<Position>> GetAllAsync(int page = 0, int offset = 10,
		Dictionary<string, object>? param = null,
		CancellationToken cancellationToken = default)
	{
		var task = await Task.Run(async () =>
		{
			using var con = _dbContext.CreateConnection();
			var query =
				"""
				SELECT "Id",
				       "UserCreatorId",
				       "CreationTime",
				       "State",
				       "UserModifierId",
				       "DateLastModify",
				       "Description",
				       "ClientId",
				       "ClientDescription",
				       "PositionLevel"
				FROM "Position" /**where**/ ORDER BY "CreationTime" DESC
				                OFFSET @Offset
				                FETCH NEXT @PageSize ROWS ONLY;
				""";
			var sb = new SqlBuilder();
			var template = sb.AddTemplate(query);


			if (param != null)
				foreach (var fields in param)
				{
					sb.Where($"{fields.Key} LIKE @{fields.Key}", fields.Value);
					Console.WriteLine(fields.Key);
				}

			sb.Where("State = @State", true);

			var result = await con.QueryAsync<Position>(template.RawSql, new
			{
				Offset = (page < 1 ? 0 : page - 1) * offset,
				PageSize = offset,
				template.Parameters
			});

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
				SET "UserModifierId" = @UserModifiedId,
				    "DateLastModify" = @DateLastModified,
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