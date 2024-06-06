using FaceVerifyAttendanceSystem.DAL.Specifications.Interfaces;
using System.Linq.Expressions;

namespace FaceVerifyAttendanceSystem.DAL.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get();
        Task<ICollection<TEntity>> GetAllAsync(ISpecification<TEntity>? spec = null);
        Task<TEntity?> GetByIdAsync(object id);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(ICollection<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(ICollection<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task DeleteByIdAsync(object id);
        Task DeleteRangeAsync(ICollection<TEntity> entities);
        Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> Pagination(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, int pageNumber, int pageSize);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> GetRandomAsync(int count);
        Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(params Expression<Func<TEntity, object>>[] includeProperties);
    }
}