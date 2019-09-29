using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AspnetCoreSPATemplate.Helpers.Classes;

namespace AspnetCoreSPATemplate.Helpers
{
    public static class ExpressionHelpers<TEntity> where TEntity : class
    {
        private static readonly MethodInfo ContainsMethod = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
        
        // build a filter expression
        public static Expression<Func<TEntity, bool>> GenerateFilter(List<Filter> filters)
        {
            Expression filterExpression = null;

            var parameter = Expression.Parameter(typeof(TEntity), "p");
            foreach (Filter searchTerm in filters)
            {
                var property = typeof(TEntity).GetProperty(searchTerm.PropertyName);
                if (property == null)
                {
                    return null;
                }

                var propertyAccess = Expression.MakeMemberAccess(parameter, property);

                switch (searchTerm.RelationalOperator)
                {
                    case Enums.RelationalOperator.TextLike:
                        MethodCallExpression methodCallExpression = GetLikeExpression(propertyAccess, searchTerm.Value);
                        filterExpression = GetMethodCallExpression(searchTerm.LogicalOperator, filterExpression, methodCallExpression);
                        break;
                    // TODO: add more relation operator here
                }
            }
            if (filterExpression != null)
            {
                Expression<Func<TEntity, bool>> predicate = Expression.Lambda<Func<TEntity, bool>>(filterExpression, parameter);
                return predicate;
            }
            return null;
        }

        // build a oderBy expression
        public static Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> GenerateOrderBy(string orderBy, string orderType)
        {
            var typeQueryable = typeof(IQueryable<TEntity>);
            var argQueryable = Expression.Parameter(typeQueryable, "p");
            var outerExpression = Expression.Lambda(argQueryable, argQueryable);
            var props = orderBy.Split('.');

            var type = typeof(TEntity);
            var arg = Expression.Parameter(type, "x");

            Expression expr = arg;
            foreach (var prop in props)
            {
                var pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                expr = Expression.Property(expr, pi ?? throw new InvalidOperationException());
                type = pi.PropertyType;
            }
            var lambda = Expression.Lambda(expr, arg);
            var methodName = orderType.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";

            var resultExp =
                Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(TEntity), type }, outerExpression.Body, Expression.Quote(lambda));
            var finalLambda = Expression.Lambda(resultExp, argQueryable);
            return (Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>)finalLambda.Compile();
        }

        private static Expression GetMethodCallExpression(Enums.LogicalOperator logicalOperator, Expression filterExpression, MethodCallExpression methodCallExpression)
        {
            switch (logicalOperator)
            {
                case Enums.LogicalOperator.And:
                    filterExpression = filterExpression == null
                        ? (Expression) methodCallExpression
                        : Expression.And(filterExpression, methodCallExpression);
                    break;
                case Enums.LogicalOperator.Or:
                    filterExpression = filterExpression == null
                        ? (Expression) methodCallExpression
                        : Expression.Or(filterExpression, methodCallExpression);
                    break;
                default:
                    filterExpression = filterExpression == null
                        ? (Expression) methodCallExpression
                        : Expression.And(filterExpression, methodCallExpression);
                    break;
            }
            return filterExpression;
        }

        private static MethodCallExpression GetLikeExpression(MemberExpression propertyAccess, string columnValue)
        {
            MethodCallExpression methodCallExpression = Expression.Call(GetLowerCasePropertyAccess(propertyAccess), ContainsMethod, Expression.Constant(columnValue.ToLower()));
            return methodCallExpression;
        }

        private static MethodCallExpression GetLowerCasePropertyAccess(MemberExpression propertyAccess)
        {
            return Expression.Call(Expression.Call(propertyAccess, "ToString", new Type[0]), typeof(string).GetMethod("ToLower", new Type[0]) ?? throw new InvalidOperationException());
        }
    }
}
