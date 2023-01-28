using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cybersoft.Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity, Tkey> specification)
        {
            if (inputQuery == null)
            {
                throw new ArgumentNullException(nameof(inputQuery), "Parameter cannot be null");
            }

            var query = inputQuery;

            if (specification == null)
            {
                return query;
            }

            // modify the iqueryable using the specifications criteria expression
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            //Include all expression-based includes
            query = specification.Includes.Aggregate(query,
                                                        (current, include) => current.Include(include));

            // Include any string-based include statements
            query = specification.IncludeStrings.Aggregate(query,
                                    (current, include) => current.Include(include));

            // Apply ordering if expressions are set
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            // If skip is 0, avoid adding to the IQueryable. It will generate more optimized SQL that way
            if (specification.Skip != null && specification.Skip != 0)
            {
                query = query.Skip(specification.Skip.Value);
            }

            if (specification.Take != null)
            {
                query = query.Take(specification.Take.Value);
            }
            return query;
        }
    }
}
