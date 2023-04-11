using System.Runtime.InteropServices.JavaScript;
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
                     "UserModifiedId",
                     "DateLastModified",
                     "SkillId",
                     "PositionId",
                     "SkillName",
                     "MinToAccept",
                     "PositionSkillType")
                VALUES
                    (@UserCreatorId,
                     @CreationTime,
                     @State,
                     @UserModifiedId,
                     @DateLastModified,
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
					UserModifiedId = Guid.NewGuid(),
					DateLastModified = DateTime.UtcNow,
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
	public async Task<IEnumerable<PositionSkill>> GetAllAsync(object? param = null,
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
				       "SkillId",
				       "PositionId",
				       "SkillName",
				       "MinToAccept",
				       "PositionSkillType"
				FROM "PositionSkill" WHERE "State" = @IsActive
				                     ORDER BY "CreationTime" DESC
				                	LIMIT 300 OFFSET 0;
				""";
			using var con = _dbContext.CreateConnection();
			var result = await con.QueryAsync<PositionSkill>(query, param);
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
				SELECT "UserCreatorId",
				       "CreationTime",
				       "State",
				       "UserModifiedId",
				       "DateLastModified",
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
				SET "UserModifiedId" = @UserModifiedId,
				    "DateLastModified" = @DateLastModified,
				    "SkillId" = @SkillId,
				    "PositionSkillType"= @PositionId,
				    "SkillName"= @SkillName,
				    "MinToAccept" = @MinToAccept,
				    "PositionSkillType" = @PositionSkillType
				WHERE "Id" = @Id;
				""";
			using var con = _dbContext.CreateConnection();
			var result = await con.ExecuteAsync(sql, new
			{
				Id = id,
				UserModifiedId = Guid.NewGuid(),
				DateLastModified = DateTime.UtcNow,
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