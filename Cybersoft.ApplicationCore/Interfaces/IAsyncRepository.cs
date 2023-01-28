using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Interfaces
{
    public interface IAsyncRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey Id);

        Task<TEntity> AddAsync(TEntity entity);

        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task DeleteRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> FirstOrDefaultAsync(ISpecification<TEntity, TKey> spec);

        Task<int> CountAsync(ISpecification<TEntity, TKey> spec);

        Task<IReadOnlyList<TEntity>> ListAllAsync();

        Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity, TKey> spec);

        Task<IReadOnlyList<TEntity>> Find(Expression<Func<TEntity, bool>> expression);


        int Count(Expression<Func<TEntity, bool>> predicate);

        bool Contains(Expression<Func<TEntity, bool>> predicate);


        IEnumerable<TEntity> GetPaginatedItems(PaginationParams ownerParameters);

        Task<IEnumerable<TEntity>> GetPaginatedItemsWithOrderByDescendingIdAsync(PaginationParams ownerParameters);

        Task<IEnumerable<TEntity>> GetPaginatedItemsWithFilterAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int first = 0, int offset = 0);

        //Task<IEnumerable<TEntity>> GetPaginatedItemsWithoutIncludeAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int first = 0, int offset = 0);

    }
}


///// <summary>
///// IAsyncRepository
///// </summary>
///// <typeparam name="T"></typeparam>
//public interface IAsyncRepository<T> where T : BaseEntity
//{
//    Task<T> GetByIdAsync(int id);
//    Task<T> FirstOrDefaultAsync(ISpecification<T> spec);
//    Task<IReadOnlyList<T>> ListAllAsync();
//    Task<List<T>> ListAsync(ISpecification<T> spec);
//    Task<T> AddAsync(T entity);
//    Task UpdateAsync(T entity);
//    Task<T> GetSingleBySpec(ISpecification<T> spec);
//}
//}