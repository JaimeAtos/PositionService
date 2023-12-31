using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Persistence.Context;

namespace Persistence.Repositories;

public class PositionSkillRepository : IPositionSkillRepository
{
	private readonly PositionDbContext _dbContext;

	public PositionSkillRepository(PositionDbContext context)
	{
		_dbContext = context;
	}

	public async Task<Guid> CreateAsync(PositionSkill entity, CancellationToken cancellationToken = default)
	{
		var task = await Task.Run(async () =>
		{
			var sql =
				"""
                INSERT INTO "PositionSkill"
                    ("UserCreatorId",
                     "CreationTime",
                     "State",
                     "UserModifierId",
                     "DateLastModify",
                     "SkillId",
                     "PositionId",
                     "SkillName",
                     "MinToAccept",
                     "PositionSkillType")
                VALUES
                    (@UserCreatorId,
                     @CreationTime,
                     @State,
                     @UserModifierId,
                     @DateLastModify,
                     @SkillId,
                     @PositionId,
                     @SkillName,
                     @MinToAccept,
                     @PositionSkillType)
                RETURNING "Id";
                """;
			using var con = _dbContext.CreateConnection();
			var result = await con.ExecuteScalarAsync<Guid>(sql,
				new
				{
					UserCreatorId = Guid.NewGuid(),
					CreationTIme = DateTime.UtcNow,
					State = true,
					UserModifierId = Guid.NewGuid(),
					DateLastModify = DateTime.UtcNow,
					entity.SkillId,
					entity.PositionId,
					entity.SkillName,
					entity.MinToAccept,
					entity.PositionSkillType
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
				UPDATE "PositionSkill"
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
				DateLastModify = DateTime.UtcNow,
			});
			return result > 0;
		}, cancellationToken);
		return task;
	}
	
	public async Task<IEnumerable<PositionSkill>> GetAllAsync(Dictionary<string, object> param,
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
				       "SkillId",
				       "PositionId",
				       "SkillName",
				       "MinToAccept",
				       "PositionSkillType"
				FROM "PositionSkill" /**where**/
				;
				""";
			
			var sb = new SqlBuilder();
			var template = sb.AddTemplate(query);
			
			foreach (var key in param.Select(field => field.Key))
			{
				sb.Where($$"""
							"{{key}}" = @{{key}}
						""");
			}
			
			var parameters = new DynamicParameters(param);
			using var con = _dbContext.CreateConnection();
			var result = await con.QueryAsync<PositionSkill>(template.RawSql, parameters);
			return result;
		}, cancellationToken);

		return task;
	}

	public async Task<IEnumerable<PositionSkill>> GetAllAsync(int page, int offset, Dictionary<string, object> param,
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
				       "SkillId",
				       "PositionId",
				       "SkillName",
				       "MinToAccept",
				       "PositionSkillType"
				FROM "PositionSkill" /**where**/
				                     OFFSET @Offset
				                     FETCH NEXT @PageSize ROWS ONLY;
				""";
			
			var sb = new SqlBuilder();
			var template = sb.AddTemplate(query);
			
			foreach (var key in param.Select(field => field.Key))
			{
				sb.Where($$"""
							"{{key}}" = @{{key}}
						""");
			}
			param.Add("Offset", (page < 1? 0 : page - 1) * offset);
			param.Add("PageSize", offset);
			
			var parameters = new DynamicParameters(param);
			using var con = _dbContext.CreateConnection();
			var result = await con.QueryAsync<PositionSkill>(template.RawSql, parameters);
			return result;
		}, cancellationToken);

		return task;
	}

	public async Task<PositionSkill> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken = default)
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
				       "SkillId",
				       "PositionId",
				       "SkillId",
				       "MinToAccept",
				       "PositionSkillType" 
				FROM "PositionSkill" WHERE "Id" = @PositionId;
				""";
			using var con = _dbContext.CreateConnection();
			var result = await con.QueryFirstOrDefaultAsync<PositionSkill>(query, new { PositionId = id });
			return result;
		}, cancellationToken);

		return task;
	}

	public async Task<bool> UpdateAsync(PositionSkill entity, Guid id, CancellationToken cancellationToken = default)
	{
		var task = await Task.Run(async () =>
		{
			var sql =
				"""
				UPDATE "PositionSkill"
				SET "UserModifierId" = @UserModifierId,
				    "DateLastModify" = @DateLastModify,
				    "SkillId" = @SkillId,
				    "PositionId"= @PositionId,
				    "SkillName"= @SkillName,
				    "MinToAccept" = @MinToAccept,
				    "PositionSkillType" = @PositionSkillType
				WHERE "Id" = @Id;
				""";
			using var con = _dbContext.CreateConnection();
			var result = await con.ExecuteAsync(sql, new
			{
				Id = id,
				UserModifierId = Guid.NewGuid(),
				DateLastModify = DateTime.UtcNow,
				entity.SkillId,
				entity.PositionId,
				entity.SkillName,
				entity.MinToAccept,
				entity.PositionSkillType
			});
			return result > 0;
		}, cancellationToken);
		return task;
	}
}
