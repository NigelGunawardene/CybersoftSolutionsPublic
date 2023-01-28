using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Interfaces;
using Cybersoft.ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;


namespace Cybersoft.Infrastructure.Data
{
    public class EfRepository<TEntity, TKey> : IAsyncRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly CyberSoftContext _dbContext;

        public EfRepository(CyberSoftContext cyberMuffinContext)
        {
            _dbContext = cyberMuffinContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
            await _dbContext.SaveChangesAsync();
            _dbContext.Entry(entities).State = EntityState.Detached;
            return entities;
        }

        public async Task UpdateAsync(TEntity id)
        {
            _dbContext.Entry(id).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity id)
        {
            _dbContext.Set<TEntity>().Remove(id);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity, TKey> spec)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecification<TEntity, TKey> spec)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.CountAsync();
        }

        public async Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity, TKey> spec)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
        }

        public async Task<TEntity> LastOrDefaultAsync(ISpecification<TEntity, TKey> spec)
        {
            var specificationResult = ApplySpecification(spec);
            return await specificationResult.LastOrDefaultAsync();
        }

        public Task UpdateAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<TEntity>> ListAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync().ConfigureAwait(false);
        }

        public Task<IReadOnlyList<TEntity>> Find(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).Count();
        }

        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return Count(predicate) > 0 ? true : false;
        }

        public IEnumerable<TEntity> GetPaginatedItems(PaginationParams ownerParameters)
        {
            return _dbContext.Set<TEntity>()
                .OrderBy(db => db.Id)
                .Skip((ownerParameters.PageNumber - 1) * ownerParameters.PageSize)
                .Take(ownerParameters.PageSize)
                .ToList();
        }

        public async Task<IEnumerable<TEntity>> GetPaginatedItemsWithOrderByDescendingIdAsync(PaginationParams ownerParameters)
        {
            return await _dbContext.Set<TEntity>()
                  .OrderBy(db => db.Id)
                  .Skip((ownerParameters.PageNumber - 1) * ownerParameters.PageSize)
                  .Take(ownerParameters.PageSize)
                  .ToListAsync();
        }


        public virtual async Task<IEnumerable<TEntity>> GetPaginatedItemsWithFilterAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "",
        int first = 0, int offset = 0)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (offset > 0)
            {
                query = query.Skip((offset - 1) * first);
            }
            if (first > 0)
            {
                query = query.Take(first);
            }


            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity, TKey> spec)
        {
            var evaluator = new SpecificationEvaluator<TEntity, TKey>();
            return evaluator.GetQuery(_dbContext.Set<TEntity>().AsQueryable(), spec);
        }
    }
}
