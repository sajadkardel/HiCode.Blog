using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HC.Data.Repositories.Contracts;

public interface IRepository<TEntity> where TEntity : class
{
    DbSet<TEntity> Entities { get; }
    IQueryable<TEntity> Table { get; }
    IQueryable<TEntity> TableNoTracking { get; }

    void Add(TEntity entity, bool saveNow = true);
    Task AddAsync(TEntity entity, bool saveNow = true, CancellationToken cancellationToken = default);
    void AddRange(IEnumerable<TEntity> entities, bool saveNow = true);
    Task AddRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true, CancellationToken cancellationToken = default);
    void Attach(TEntity entity);
    void Delete(TEntity entity, bool saveNow = true);
    Task DeleteAsync(TEntity entity, bool saveNow = true, CancellationToken cancellationToken = default);
    void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true, CancellationToken cancellationToken = default);
    void Detach(TEntity entity);
    TEntity GetById(params object[] ids);
    ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken = default, params object[] ids);
    void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty) where TProperty : class;
    Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken = default) where TProperty : class;
    void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty) where TProperty : class;
    Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken = default) where TProperty : class;
    void Update(TEntity entity, bool saveNow = true);
    Task UpdateAsync(TEntity entity, bool saveNow = true, CancellationToken cancellationToken = default);
    void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool saveNow = true, CancellationToken cancellationToken = default);
}