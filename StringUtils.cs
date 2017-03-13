using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
    public class StringHelper
    {
        public static string RandomString(int length)
        {

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxy";
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var epochLong = Convert.ToInt64((DateTime.UtcNow - epoch).TotalMilliseconds);

            var random = new Random((int)epochLong);

            return new string(Enumerable.Repeat(chars, length)
                 .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
    public static class StringUtils
    {


        public static bool IsNullOrWhiteSpace(this string v)
        {
            return string.IsNullOrWhiteSpace(v);
        }

        public static int StringToInteger(this string v)
        {
            var chars = v.ToCharArray().Where(i => char.IsNumber(i));
            if (chars.Count() < 1)
                return -1;
            return int.Parse(new String(chars.ToArray()));
        }
        public static long StringToLong(this string v)
        {
            var chars = v.ToCharArray().Where(i => char.IsNumber(i));
            if (chars.Count() < 1)
                return -1;

            return long.Parse(new String(chars.Take(19).ToArray()));
        }
        public static IEnumerable<string> StringToLines(this string v)
        {

            return v.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string RemoveEmptyLines(this string value)
        {

            return string.Join(Environment.NewLine, value.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
        }

    }
}
