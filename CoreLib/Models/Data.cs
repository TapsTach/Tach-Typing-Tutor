using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CoreExtLib;
using Dapper;
namespace CoreLib
{
    public static class Database
    {
        public static string DataFolder { get; set; }
        static Database()
        {
            DataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Tach Typing Tutor");
            string file = $@"Database.db";
            string path = Path.Combine(DataFolder, file);
            ConnectionString = $@"Data Source= {path};Pooling=true;FailIfMissing=false";
        }

        public static string ConnectionString { get; set; }


        public static List<T> Query<T>(string constraint = "") where T : new()
        {
            List<T> retList = new List<T>();
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);

            string tableName = GetTableName(typeof(T));

            using (connection)
            {
                try
                {
                    retList = connection.Query<T>($"select * from {tableName} {constraint}").ToList();
                }
                catch (Exception e)
                {

                }
            }
            return retList;
        }

        public static void Insert<T>(List<T> objects) where T : new()
        {
            foreach (var item in objects)
            {
                Insert<T>(item);
            }

        }
        public static void Insert<T>(T item) where T : new()
        {
            Type type = typeof(T);
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            SQLiteCommand comm = connection.CreateCommand();

            var mappings = GetPropertyMappings(type);
            List<string> mappingsToRemove =
                new List<string>();

            string tableName = GetTableName(type);
            //remove all id mappings. Id has to be auto populated--
            foreach (var mp in mappings)
            {
                if (mp.Key.ToLower() == "id"
                     || mp.Key.ToLower() == $"{tableName}id"
                     || mp.Key.ToLower() == $"{type.Name}id")
                    mappingsToRemove.Add(mp.Key);

                if (mp.Value.GetValue(item) == null && !mappingsToRemove.Contains(mp.Key))
                    mappingsToRemove.Add(mp.Key);
            }

            foreach (var mp in mappingsToRemove)
            {
                mappings.Remove(mp);
            }
            //------------------------------------------------------

            string fieldsString = mappings.Keys.Aggregate((a, b) => a + ", " + b);

            string values = mappings.Keys.Combine(" ,@").TrimStart().TrimStart(',');

            var parValMapping = new Dictionary<string, string>();

            foreach (var mp in mappings)
            {
                parValMapping.Add($"@{mp.Key}", $"{mp.Value.GetValue(item)}");
            }

            comm.CommandText = ($"insert into {tableName} ({fieldsString}) values({values}) ;");

            try
            {
                foreach (var vp in parValMapping)
                {
                    comm.Parameters.AddWithValue(vp.Key, vp.Value);
                }
            }
            catch { }

            using (connection)
            {
                connection.Open();
                comm.ExecuteNonQuery();
            }

        }

        public static void DeleteAll<T>(string constraint = null)
        {

            Type type = typeof(T);
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            SQLiteCommand comm = connection.CreateCommand();
            string tableName = GetTableName(type);

            using (connection)
            {
                connection.Open();
                comm.CommandText = $"delete from {tableName} {constraint}";
                comm.ExecuteNonQuery();
            }
        }

        public static void Delete<T> (List<T> items) where T:new()
        {
            foreach (var item in items)
                Delete(item);
        }

        public static void Delete<T> (T item, string constraint = null) where T: new()
        {
            //Type type = typeof(T);
            Type type = typeof(T);

            //Getting the table name.
            string tableName = GetTableName(type);

            //getting properties and their respective database field names.
            var mappings = GetPropertyMappings(type);
            KeyValuePair<string, PropertyInfo> idMapping = new KeyValuePair<string, PropertyInfo>();

            foreach (var mp in mappings)
            {
                if (mp.Key.ToLower() == "id"
                     || mp.Key.ToLower() == $"{tableName}id"
                     || mp.Key.ToLower() == $"{type.Name}id")
                {
                    idMapping = mp;
                    break;
                }
            }

          
            if (constraint == null && idMapping.Key != null)
            {
                int idValue = (int)idMapping.Value.GetValue(item);
                string idName = idMapping.Key;
                constraint = $"where {idName} = {idValue}";
            }

            //creating connection
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            SQLiteCommand comm = connection.CreateCommand();

            //delete from user where id =  2
            comm.CommandText = ($"delete from {tableName} {constraint};");

            //executing.
            using (connection)
            {
                try
                {
                    connection.Open();
                    comm.ExecuteNonQuery();
                }
                catch (Exception e)
                {

                }
            }
        }

        public static void Update<T>(List<T> elements) where T : new()
        {
            foreach (var letter in elements)
                Update(letter);
        }
        public static void Update<T>(T item, string constraint = null) where T : new()
        {
            //Type type = typeof(T);
            Type type = typeof(T);

            //Getting the table name.
            string tableName = GetTableName(type);

            //getting properties and their respective database field names.
            var mappings = GetPropertyMappings(type);
            List<string> mappingsToRemove =
                new List<string>();
            KeyValuePair<string, PropertyInfo> idMapping = new KeyValuePair<string, PropertyInfo>();
            //remove all id mappings.----------------------------------------------
            foreach (var mp in mappings)
            {
                // Id has to be auto populated--
                if (mp.Key.ToLower() == "id"
                     || mp.Key.ToLower() == $"{tableName}id"
                     || mp.Key.ToLower() == $"{type.Name}id")
                {
                    idMapping = mp;
                    mappingsToRemove.Add(idMapping.Key);
                }

                //if value is not specified on the item's property, probably the database value accepts null
                if (mp.Value.GetValue(item) == null && !mappingsToRemove.Contains(mp.Key))
                    mappingsToRemove.Add(mp.Key);
            }

            foreach (var mp in mappingsToRemove)
            {
                mappings.Remove(mp);
            }
            //---------------------------------------------------------------------



            //mapping parameters and its values;
            var parValMapping = new Dictionary<string, string>();
            foreach (var mp in mappings)
            {
                parValMapping.Add($"@{mp.Key}", $"{mp.Value.GetValue(item)}");
            }

            //creating connection
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            SQLiteCommand comm = connection.CreateCommand();

            if (constraint == null && idMapping.Key != null)
            {
                int idValue = (int)idMapping.Value.GetValue(item);
                string idName = idMapping.Key;
                constraint = $"where {idName} = {idValue}";
            }

            StringBuilder statementBuilder = new StringBuilder();
            foreach (var field in mappings.Keys)
            {
                statementBuilder.Append($"{field} = @{field},");
            }

            //update UserAccount set username = 'tinashe', password= 'tapiwa' where id =  2
            comm.CommandText = ($"update {tableName} set {statementBuilder.ToString().TrimEnd(',')} {constraint};");

            try
            {
                //adding parameters on command parameters from value and parameter mappings
                foreach (var vp in parValMapping)
                {
                    comm.Parameters.AddWithValue(vp.Key, vp.Value);

                }
            }
            catch (Exception e)
            {
            }

            //executing.
            using (connection)
            {
                try
                {
                    connection.Open();
                    comm.ExecuteNonQuery();
                }
                catch (Exception e)
                {

                }
            }
        }

        static string GetTableName(Type type)
        {
            string tableName = null;
            var attribute = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            if (attribute != null)
            {
                tableName = attribute.TableName;
            }
            else
                tableName = type.Name;

            return tableName;
        }

        static Dictionary<string, PropertyInfo> GetPropertyMappings(Type type)
        {
            var mappableProperties = type.GetProperties().Where(g => g.GetCustomAttribute(typeof(NotMappedAttribute)) == null).ToList();
            var mappingDictionary = new Dictionary<string, PropertyInfo>();
            var databaseFields = new string[mappableProperties.Count()];

            for (int i = 0; i < mappableProperties.Count(); i++)
            {
                //checking for mapping at property level
                var property = mappableProperties[i];
                var propertyMapping = property.GetCustomAttribute(typeof(PropertyMappingAttribute)) as PropertyMappingAttribute;
                if (propertyMapping != null)
                {
                    databaseFields[i] = propertyMapping.DBFieldName;

                }
                else
                {
                    //Checking for mappings at class level
                    var mapping = type.GetCustomAttributes(typeof(MappingAttribute)).Select(a => a as MappingAttribute).Where(a => a.PropertyName == property.Name).FirstOrDefault();
                    if (mapping != null)
                    {
                        databaseFields[i] = mapping.DBFieldName;

                    }
                    else
                        //if there are no any mappings found at both class and property level, use the propertyName;
                        databaseFields[i] = property.Name;
                }
                mappingDictionary.Add(databaseFields[i], property);
            }
            return mappingDictionary;
        }
        /*
   public static List<T> Query<T>( string tableName = null, string constraint = "") where T : new()
        {
            
            List<T> retList = new List<T>();
            T theClass = new T();
            Type theClassType = theClass.GetType();
            //filtering properties with NotmappedAttribute 
            var mappableProperties = theClassType.GetProperties().Where(g => g.GetCustomAttribute(typeof(NotMappedAttribute)) == null).ToList();
            var mappingDictionary = new Dictionary<string, PropertyInfo>();
            var databaseFields = new string[mappableProperties.Count()];

            //getting the table name
            if (tableName == null)
            {
                tableName = theClassType.Name;
                var tableAttribute = theClassType.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
                if (tableAttribute != null)
                    tableName = tableAttribute.TableName;
            }

            //getting the database fields from mappings
            for (int i = 0; i < mappableProperties.Count(); i++)
            {
                //checking for mapping at property level
                var property = mappableProperties[i];
                var propertyMapping = property.GetCustomAttribute(typeof(PropertyMappingAttribute)) as PropertyMappingAttribute;
                if (propertyMapping != null)
                {
                    databaseFields[i] = propertyMapping.DBFieldName;

                }
                else
                {
                    //Checking for mappings at class level
                    var mapping = theClassType.GetCustomAttributes(typeof(MappingAttribute)).Select(a => a as MappingAttribute).Where(a => a.PropertyName == property.Name).FirstOrDefault();
                    if (mapping != null)
                    {
                        databaseFields[i] = mapping.DBFieldName;

                    }
                    else
                        //if there are no any mappings found at both class and property level, use the propertyName;
                        databaseFields[i] = property.Name;
                }
                mappingDictionary.Add(databaseFields[i], property);
            }

            //Querying data
            string dbFieldsString = databaseFields.Aggregate((a, b) => a + "," + b);

            
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            SQLiteCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = $"select {dbFieldsString} from {tableName} {constraint}";
            SQLiteDataReader reader = command.ExecuteReader();

            //populating list of return type
            while (reader.Read())
            {
                T t = new T();
                foreach (var kvp in mappingDictionary)
                {
                    //int i = Convert.ToInt32(reader[kvp.Key]);
                    kvp.Value.SetValue(t, reader[kvp.Key]);
                }
                retList.Add(t);
            }
            //closing connection
            reader.Close();
            connection.Close();

            return retList;
        }
        */
    }
}