using System.Linq.Expressions;
using UzEx.Analytics.Application.Models.Pagination;

namespace UzEx.Analytics.Application.Extensions
{
    public static class QueryableSorter
    {
        private const string ASCENDING = "Ascending";
        private const string DESCENDING = "Descending";

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> queryable, List<SortCriterion>? sortCriteria, string? defaultSortProperty = "Id")
        {
            if (sortCriteria == null || sortCriteria.Count == 0)
            {
                // default sort
                return SortBy(queryable, defaultSortProperty, false);
            }

            IOrderedQueryable<T>? orderedQueryable = null;

            bool isFirstCriterion = true;

            foreach (var criterion in sortCriteria)
            {
                if (string.IsNullOrEmpty(criterion.PropertyName.Trim()))
                {
                    continue;
                }

                bool isDescending = (criterion.SortOrder == DESCENDING);

                if (isFirstCriterion)
                {
                    // OrderBy
                    orderedQueryable = SortBy(queryable, criterion.PropertyName, isDescending);
                    isFirstCriterion = false;
                }
                else
                {
                    // ThenBy
                    orderedQueryable = ThenBy(orderedQueryable, criterion.PropertyName, isDescending);
                }
            }

            return orderedQueryable ?? (IOrderedQueryable<T>)queryable;
        }

        private static IOrderedQueryable<T> SortBy<T>(IQueryable<T> queryable, string propertyName, bool descending)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.PropertyOrField(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = descending ? "OrderByDescending" : "OrderBy";

            var methodCallExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { typeof(T), property.Type },
                queryable.Expression,
                Expression.Quote(lambda));

            return (IOrderedQueryable<T>)queryable.Provider.CreateQuery<T>(methodCallExpression);
        }

        private static IOrderedQueryable<T> ThenBy<T>(IOrderedQueryable<T> orderedQueryable, string propertyName, bool descending)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.PropertyOrField(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = descending ? "ThenByDescending" : "ThenBy";

            var methodCallExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { typeof(T), property.Type },
                orderedQueryable.Expression,
                Expression.Quote(lambda));

            return (IOrderedQueryable<T>)orderedQueryable.Provider.CreateQuery<T>(methodCallExpression);
        }
    }
}
