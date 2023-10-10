using System.Linq.Expressions;


namespace DatDotnetConduit.Infrasturcture.LinQ
{
    public static class IQueryable
    {
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
            {
                return source.Where(predicate);
            }
            return source;
        }
    }
}