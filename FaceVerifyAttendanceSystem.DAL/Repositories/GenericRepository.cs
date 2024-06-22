using FaceVerifyAttendanceSystem.DAL.Context;
using FaceVerifyAttendanceSystem.DAL.Repositories.Interfaces;
using FaceVerifyAttendanceSystem.DAL.Specifications.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FaceVerifyAttendanceSystem.DAL.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApiDbContext _context;

        public GenericRepository(ApiDbContext context) => _context = context;

        public IQueryable<TEntity> Get() => _context.Set<TEntity>();

        public async Task AddAsync(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity);

        public async Task DeleteByIdAsync(object id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
                Delete(entity);
        }

        public async Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _context.Set<TEntity>().Where(predicate).ToListAsync();

        public void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);

        public async Task DeleteAsync(TEntity entity) => await Task.Run(() => Delete(entity));
        
        public async Task<ICollection<TEntity>> GetAllAsync(ISpecification<TEntity>? spec = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (spec != null)
            {
                query = ApplySpecification(query, spec);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(object id) => await _context.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);
        
        public async Task UpdateAsync(TEntity entity) => await Task.Run(() => Update(entity));

        private IQueryable<TEntity> ApplySpecification(IQueryable<TEntity> query, ISpecification<TEntity> spec)
        {
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);

            if (spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);

            return query;
        }

        public async Task<IEnumerable<TEntity>> Pagination(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, int pageNumber, int pageSize)
        {
            var query = _context.Set<TEntity>().Where(filter).OrderBy(orderBy).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return await query.ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().CountAsync(filter);
        }

        public IQueryable<TEntity> GetWithIncludes(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public async Task<IEnumerable<TEntity>> GetRandomAsync(int count)
        {
            return await _context.Set<TEntity>()
                .OrderBy(r => Guid.NewGuid())
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }

    }
}