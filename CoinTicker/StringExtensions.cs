using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoinTicker
{
    public static class StringExtensions
    {
        public static decimal TryParse(this string input, decimal defaultvalue) {
            decimal d;
            if (decimal.TryParse(input,out d)) {
                return d;
            }
            return defaultvalue;
        }

        public static int TryParse(this string input, int defaultvalue)
        {
            int d;
            if (int.TryParse(input, out d))
            {
                return d;
            }
            return defaultvalue;
        }

        public static DateTime TryParse(this string input, DateTime defaultvalue)
        {
            DateTime d;
            if (DateTime.TryParse(input, out d))
            {
                return d;
            }
            return defaultvalue;
        }

        public static DateTime FromUnixTime(this string input)
        {
            DateTime startDate = new DateTime(1970, 1, 1);
            return startDate.AddSeconds(input.TryParse(0)).ToUniversalTime();
        }

       

    }
}
