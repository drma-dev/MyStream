using System;

namespace MyStream.Helper
{
    public static class AttributesHelper
    {
        public static DateTime? GetDate(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            else if (!DateTime.TryParse(value, out _))
                return DateTime.MinValue;
            else
                return DateTime.Parse(value);
        }

        public static string FormatRuntime(this int runtime)
        {
            if (runtime == 0) return "";
            var time = TimeSpan.FromMinutes(runtime);
            return $"{time.Hours}h {time.Minutes}m";
        }
    }
}