using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreExtLib
{
    public static class NemericHelpers
    {
        public static bool IsInt(this string str)
        {

            int passed;
            return (int.TryParse(str, out passed));
        }
        public static bool IsInt(this char ch)
        {
            int passed;
            return (int.TryParse(ch.ToString(), out passed));
        }
        public static bool IsNumber(this string str)
        {
            double passed;
            return (double.TryParse(str, out passed));
        }
        public static bool IsDecimal(this string str)
        {
            decimal passed;
            return decimal.TryParse(str, out passed);
        }
        public static bool IsNumber(this char ch)
        {
            double passed;
            return (double.TryParse(ch.ToString(), out passed));
        }

        public static double CastToDouble(this object obj) => (double)obj;
        public static int CastToInt(this object obj) => (int)obj;
        public static float CastToFloat(this object obj) => (float)obj;
        public static decimal CastToDecimal(this object obj) => (decimal)obj;

        public static int ConvertToInt(this object obj) => Convert.ToInt32(obj);
        public static double ConvertToDouble(this object obj) => Convert.ToDouble(obj);
        public static decimal ConvertToDecimal(this object obj) => Convert.ToDecimal(obj);
        public static float ConvertToFloat(this object obj) => Convert.ToSingle(obj);

    }
}

