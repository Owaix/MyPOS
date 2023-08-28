using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesMngmt.Utility
{
    public static class Helper
    {
        public static string DefaultZero(this string text)
        {
            if (text == "")
            {
                text = "0";
            }
            return text;
        }

        public static decimal ToDecimal(this object text)
        {
            if (text == null || text.ToString() == "")
            {
                text = "0";
            }
            return Convert.ToDecimal(text);
        }

        public static string DefaultZero(this object text)
        {
            if (text == null)
            {
                text = "0";
            }
            if (text.ToString() == "")
            {
                text = "0";
            }
            return text.ToString();
        }
    }
}
