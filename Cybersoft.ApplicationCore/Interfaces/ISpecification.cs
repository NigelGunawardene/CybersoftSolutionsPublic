using Cybersoft.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Interfaces
{
    public interface ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
        List<Expression<Func<TEntity, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDescending { get; }
        Expression<Func<TEntity, object>> OrderThenBy { get; }
        Expression<Func<TEntity, object>> GroupBy { get; }
        int? Take { get; }
        int? Skip { get; }
        bool IsPagingEnabled { get; }
    }
}

//public interface ISpecification<T>
//{
//    Expression<Func<T, bool>> Criteria { get; }
//    Expression<Func<T, object>> OrderBy { get; }

//    int Take { get; }
//    int Skip { get; }
//    bool IsPagingEnabled { get; }
//    List<Expression<Func<T, object>>> Includes { get; }
//    List<string> IncludeStrings { get; }
//}
//}

