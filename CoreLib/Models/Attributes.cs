using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MappingAttribute : Attribute
    {
        public string PropertyName { get; set; }
        public string DBFieldName { get; set; }
        public MappingAttribute(string propertyName, string dbFieldName) 
        {
            PropertyName = propertyName;
            DBFieldName = dbFieldName;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class NotMappedAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(string tableName)
        {
            TableName = tableName;
        }
        public string TableName { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyMappingAttribute : Attribute
    {
        public PropertyMappingAttribute(string dbFieldName)
        {
            DBFieldName = dbFieldName;
        }
        public string DBFieldName { get; set; }
    }
}
