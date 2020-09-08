using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Solen.Core.Application;

namespace Solen.Persistence.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, QueryObject queryObject)
        {
            if (queryObject.Page <= 0)
                queryObject.Page = 1;
            if (queryObject.PageSize <= 0)
                queryObject.PageSize = 10;

            return query.Skip((queryObject.Page - 1) * queryObject.PageSize).Take(queryObject.PageSize);
        }

        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, QueryObject queryObject,
            Dictionary<int, Expression<Func<T, object>>> columnsMap, bool isSortDescending)
        {
            if (queryObject.OrderBy == 0 || !columnsMap.ContainsKey(queryObject.OrderBy))
                return query;

            if (isSortDescending)
                return query.OrderByDescending(columnsMap[queryObject.OrderBy]);
            else
                return query.OrderBy(columnsMap[queryObject.OrderBy]);
        }
    }
}