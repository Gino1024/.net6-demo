using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Infrastructrue.Models;

namespace Infrastructrue.Extension
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> instance, string orderByString)
        {
            SortingBO sortingList = new SortingBO(orderByString);
            sortingList.orderByList.ForEach(m =>
            {
                var isExist = typeof(T).GetProperties().Any(p => p.Name.ToLower() == m.columnName.ToLower());
                if (isExist)
                {
                    if (m.isDec)
                    {
                        instance = instance.OrderByDescending(m.columnName);
                    }
                    else
                    {
                        instance = instance.OrderBy(m.columnName);
                    }
                }
            });

            return instance;
        }
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderBy(ToLambda<T>(propertyName));
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderByDescending(ToLambda<T>(propertyName));
        }

        private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
        {
            try
            {

                var parameter = Expression.Parameter(typeof(T));
                var property = Expression.Property(parameter, propertyName);
                var propAsObject = Expression.Convert(property, typeof(object));

                return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}
