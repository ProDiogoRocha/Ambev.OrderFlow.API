using MediatR;
using System.Linq.Expressions;

namespace Ambev.OrderFlow.Application.ExtensionsMethods
{
    public static class MediatorExtensionsMethods
    {
        public static T ParseFromTo<T>(this object obj) where T : class
        {
            T? t = (T?)Activator.CreateInstance(typeof(T));

            foreach (var p in t.GetType().GetProperties())
            {
                var prop = obj.GetType().GetProperty(p.Name);

                if (prop != null)
                {
                    p.SetValue(p, prop.GetValue(prop, null));
                }
            }
            return t;
        }

        public static Expression<Func<TEntity, bool>> BuildPredicateById<TEntity>(Guid id)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.PropertyOrField(parameter, "Id");
            var constant = Expression.Constant(id);
            var equal = Expression.Equal(property, constant);
            return Expression.Lambda<Func<TEntity, bool>>(equal, parameter);
        }
    }
}
