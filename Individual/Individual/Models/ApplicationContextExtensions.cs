using Individual.Models.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Individual.Models
{
    public static class ApplicationContextExtensions
    {
        private static readonly DatabaseProvider _databaseProvider = new DatabaseProvider("Server=(localdb)\\mssqllocaldb;Database=banksdb;Trusted_Connection=True;");
        public static Task<List<T>> ToListAsync<T>(this IEnumerable<T> source)
        {
            return Task.Run(() => source.ToList());
        }
        public static Task<T> FirstOrDefaultAsync<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return Task.Run(() => source.FirstOrDefault(predicate));
        }
        public static Task<T> FindAsync<T>(this IEnumerable<T> source, object id)
        {
            return Task.Run(() => source.FirstOrDefault(x =>
            {
                var type = typeof(T);
                var property = type.GetProperty("Id");
                return property.GetValue(x).Equals(id);
            }));
        }
        public static Task SaveChangesAsync(this ApplicationContext context)
        {
            return Task.CompletedTask;
        }
        
        public static void Add<T>(this ApplicationContext context, T element)
        {
            var type = element.GetType();
            context.DatabaseProvider.AddElement(element, $"{type.Name}s");
        }
        public static void Update<T>(this ApplicationContext context, T element)
        {
            var type = element.GetType();
            context.DatabaseProvider.UpdateElement(element, $"{type.Name}s");
        }

        public static void Remove<T>(this IEnumerable<T> source, T element)
        {
            var type = element.GetType();
            source.ToList().Remove(element);
            _databaseProvider.DeleteElement(element, $"{type.Name}s");
        }
        public static IEnumerable<TEntity> Include<TEntity, TProperty>(this IEnumerable<TEntity> source,
         Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            MemberExpression member = navigationPropertyPath.Body as MemberExpression;
            PropertyInfo propInfo = member.Member as PropertyInfo;
            foreach (var item in source)
            {
                var type = item.GetType();
                var nabigationProperty = type.GetProperty(propInfo.Name);
                var idProperty = type.GetProperty($"{propInfo.Name}Id");

                var destinationEntity = _databaseProvider.GetElementById(nabigationProperty.PropertyType,
                            (int)idProperty.GetValue(item),
                            $"{propInfo.Name}s");

                nabigationProperty
                    .SetValue(item,
                        destinationEntity);
            }

            return source;
        }
        public static bool ColumnExist(this SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
