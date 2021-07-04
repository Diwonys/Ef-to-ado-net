using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Individual.Models
{
    public class DatabaseProvider
    {
        private readonly SqlConnection _connection;
        public DatabaseProvider(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public IEnumerable<T> GetCollection<T>(Type entityType, string tableName)
        {
            _connection.Open();
            SqlDataReader reader = new SqlCommand($"SELECT * FROM {tableName}", _connection).ExecuteReader();

            List<object> values = new List<object>();
            InitValues(entityType, reader, values);

            reader.Close();
            _connection.Close();


            return values.Select(x => (T)x);
        }
        public void AddElement<T>(T element, string tableName)
        {
            var type = element.GetType();
            string propertyNames = string.Empty;
            string propertyValues = string.Empty;

            GetNamesValues(element, type, ref propertyNames, ref propertyValues);


            string sqlExpression = $"INSERT INTO {tableName} ({propertyNames}) VALUES ({propertyValues})";
            _connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void UpdateElement<T>(T element, string tableName)
        {
            var type = element.GetType();

            string sqlExpression = $"Update {tableName} SET ";

            foreach (var property in type.GetProperties())
            {
                if (property.Name != "Id")
                {
                    var value = property.GetValue(element);
                    if (value != null)
                    {
                        if (value is not int)
                            sqlExpression += $"{property.Name} = N'{value}',";
                        else
                            sqlExpression += $"{property.Name} = {value},";
                    }
                }
            }
            sqlExpression = sqlExpression.Remove(sqlExpression.Length - 1);

            sqlExpression += $" WHERE Id = {type.GetProperty("Id").GetValue(element)}";
            _connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void DeleteElement<T>(T element, string tableName)
        {
            var type = element.GetType();

            _connection.Open();
            SqlCommand command = new SqlCommand($"Delete {tableName} WHERE Id = {type.GetProperty("Id").GetValue(element)}", _connection);
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public object GetElementById(Type entityType, int selfId, string tableName)
        { 
            _connection.Open();
            SqlDataReader reader = new SqlCommand($"SELECT * FROM {tableName} WHERE Id = {selfId}", _connection).ExecuteReader();

            var instance = Activator.CreateInstance(entityType);
            while (reader.Read())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (reader.ColumnExist(property.Name))
                        property.SetValue(instance, reader[property.Name]);
                }
            }

            reader.Close();
            _connection.Close();

            return instance;
        }

        private static void GetNamesValues<T>(T element, Type type, ref string propertyNames, ref string propertyValues)
        {
            foreach (var property in type.GetProperties())
            {
                if (property.Name != "Id")
                {
                    var value = property.GetValue(element);
                    if (value != null)
                    {
                        if (value is not int)
                            propertyValues += "N\'" + value.ToString() + "\'" + ",";
                        else
                            propertyValues += value.ToString() + ",";
                        propertyNames += property.Name + ",";
                    }
                }
            }

            propertyValues = propertyValues.Remove(propertyValues.Length - 1);
            propertyNames = propertyNames.Remove(propertyNames.Length - 1);
        }

        private void InitValues(Type entityType, SqlDataReader reader, List<object> values)
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var instance = Activator.CreateInstance(entityType);
                    foreach (var property in entityType.GetProperties())
                    {
                        if (reader.ColumnExist(property.Name))
                            property.SetValue(instance, reader[property.Name]);
                    }
                    values.Add(instance);
                }
            }
        }


    }
}
