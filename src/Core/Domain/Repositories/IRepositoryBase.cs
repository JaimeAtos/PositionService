using Atos.Core.Abstractions;
using Atos.Core.Commons;

namespace Domain.Repositories;

public interface IRepositoryBase<TKey, TUserKey, TEntity> where TEntity : class, IEntityBase<TKey, TUserKey>
{
    Task<TEntity> GetEntityByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(int page = 0, int offset = 10, CancellationToken cancellationToken = default);
    Task<TKey> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(TEntity entity, TKey id, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
}
