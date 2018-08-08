using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreExtLib
{
    public static class CollectionsHelpers
    {
        static Random rand = new Random();

        /// <summary>
        /// set or replaces every value in a collection with the specified one.
        /// </summary>
        /// <param name="objArr">collection</param>
        /// <param name="value">specified value</param>
        public static void SetForeach(this IList<object> objList, object value)
        {
            for (int i = 0; i < objList.Count; i++)
            {
                objList[i] = value;
            }
        }

        /// <summary>
        /// set or replaces every value in a collection with the specified one.
        /// </summary>
        /// <param name="objArr">collection</param>
        /// <param name="value">specified value</param>
        public static void SetForeach(this object[] objArr, object value)
        {
            for (int i = 0; i < objArr.Length; i++)
            {
                objArr[i] = value;
            }
        }

        /// <summary>
        /// returns a random entry from a collection that implements IList interface;
        /// </summary>
        /// <typeparam name="T">Collection that implements IList interface</typeparam>
        /// <param name="elements">parameter name</param>
        /// <returns>a random entry from the collection</returns>
        /// <exception cref="">throws InvalidLenghtException if the collection does not have any value</exception>
        public static T ChooseRandomly<T>(this IList<T> elements)
        {
            if (elements.Count == 0 || elements == null)
                throw new InvalidOperationException("a collection should have at least one entry");
            return elements[rand.Next(0, elements.Count())];

        }

        /// <summary>
        /// returns a random entry from a collection that implements IList interface;
        /// </summary>
        /// <typeparam name="T">Collection that implements IList interface</typeparam>
        /// <param name="elements">parameter name</param>
        /// <returns>a random entry from the collection</returns>
        /// <exception cref="">throws InvalidLenghtException if the collection does not have any value</exception>
        public static T ChooseRandomly<T>(this T[] elements)
        {
            if (elements.Count() == 0 || elements == null)
                throw new InvalidOperationException("a collection should have at least one entry");
            return elements[rand.Next(0, elements.Count())];
        }


        /// <summary>
        /// returns a random entry from a collection that implements IList interface;
        /// </summary>
        /// <typeparam name="T">Collection that implements IList interface</typeparam>
        /// <param name="elements">parameter name</param>
        /// <returns>a random entry from the collection</returns>
        /// <exception cref="">throws InvalidLenghtException if the collection does not have any value</exception>
        public static List<T> ChooseRandomly<T>(this List<T> elements, int count)
        {
            if (elements.Count == 0 || elements == null)
                throw new InvalidOperationException("a collection should have at least one entry");
            List<T> retValue = new List<T>();
            for (int i = 0; i < count; i++)
            {
                retValue.Add(elements.ChooseRandomly());
            }
            return retValue;
        }

        /// <summary>
        /// returns a random entry from a collection that implements IList interface;
        /// </summary>
        /// <typeparam name="T">Collection that implements IList interface</typeparam>
        /// <param name="elements">parameter name</param>
        /// <returns>a random entry from the collection</returns>
        /// <exception cref="">throws InvalidLenghtException if the collection does not have any value</exception>
        public static T[] ChooseRandomly<T>(this T[] elements, int count)
        {
            if (elements.Count() == 0 || elements == null)
                throw new InvalidOperationException("a collection should have at least one entry");
            T[] retValue = new T[count];
            for (int i = 0; i < count; i++)
            {
                retValue[i] = elements.ChooseRandomly();
            }
            return retValue;
        }

        /// <summary>
        /// joins the collection into one string
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="jointString">this string will be appended in between each joint and before the first element</param>
        /// <returns></returns>
        public static string Combine(this IEnumerable<string> collection, string jointString = "")
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < collection.Count(); i++)
            {
                sb.Append(jointString);
                sb.Append(collection.ElementAt(i));
            }
            return sb.ToString();
        }

        public static string[] Split(this string str, params string[] splitString)
        {
            List<string> list = new List<string>();
            foreach (string s in splitString)
            {
                str = str.Replace(s, "♥*");
            }
            //"♥*j♥*f♥*5"
            string temp = "";
            a:
            if (str.Contains("♥*"))
            {
                if (str.StartsWith("♥*"))
                {
                    str = str.Remove(0, 2);
                }
                temp = str.Substring(0, str.IndexOf("♥*"));
                if (temp == "")
                {
                    temp = str.Substring(2);
                    str = temp;
                }
                else
                {
                    list.Add(temp);
                    str = str.Substring(str.IndexOf("♥*") + 2);
                }
                goto a;
            }
            else
            {
                if (!string.IsNullOrEmpty(temp))
                    list.Add(str);
            }
            return list.ToArray();
        }
    }
}
