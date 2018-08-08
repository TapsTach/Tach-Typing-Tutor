using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreExtLib
{
    public static class GenericHelpers
    {
        /// <summary>
        /// Copies properties from T1 to T2 where properties has similary name
        /// Or where propertyMapping name is specified
        /// </summary>
        public static void CopyPropertiesFrom<T1, T2>(this T2 copyToClass, T1 copyFromClass)
          where T1 : class, new()
          where T2 : class, new()
        {
            Type t1Type = copyFromClass.GetType();
            Type t2Type = copyToClass.GetType();

            PropertyInfo[] t1Properties = t1Type.GetProperties();

            foreach (var pinfo in t1Properties)
            {
                var propertyName = pinfo.Name;
                var t2Property = t2Type.GetProperty(propertyName);
                if (t2Property != null)
                {
                    if (t2Property.CanWrite)
                        if (t2Property.PropertyType == pinfo.PropertyType)
                        {
                            t2Property.SetValue(copyToClass, pinfo.GetValue(copyFromClass));
                        }
                }
            }
        }
    }
}
