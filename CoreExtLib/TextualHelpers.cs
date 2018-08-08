using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreExtLib
{
    public static class TextualHelpers
    {
        public static bool IsUpper(this char ch)
        {
            return (ch > 64 && ch < 91);
        }
        public static bool IsLower(this char ch)
        {
            return (ch > 96 && ch < 123);
        }
        public static char ToLower(this char ch)
        {
            if (ch.IsUpper())
                return (char)((int)ch + 32);
            return ch;
        }
        public static char ToUpper(this char ch)
        {
            if (ch.IsLower())
                return (char)((int)ch - 32);
            else return ch;
        }
        public static bool IsLower(this string str)
        {
            foreach (char c in str)
                if (!IsLower(c))
                    return false;
            return true;
        }
        public static bool IsUpper(this string str)
        {
            foreach (char c in str)
                if (!IsUpper(c))
                    return false;
            return true;
        }


        public static bool IsLetter(this char ch)
        {
            if (ch.IsLower() || ch.IsUpper())
                return true;
            else return false;
        }

        public static string CastToString(this object obj)
        {
            return (string)obj;
        }
        public static bool StartsWith(this string str, char param)
        {
            if (str.Length > 0)
                return str[0] == param;
            else return false;
        }

        /// <summary>
        /// Combines the given string [str] with [str] for a specified number of times [times]
        /// </summary>
        /// <param name="str"></param>
        /// <param name="times"></param>
        /// <param name="delimiter">string value you want to be appended between every combination</param>
        /// <returns></returns>
        public static string RepeatCombine(this string str, int times, string delimiter = "")
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(str);
            for (int i = 0; i < times; i++)
            {
                builder.Append(delimiter);
                builder.Append(str);
            }
            return builder.ToString();
        }
    }

}
