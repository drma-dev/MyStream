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
    }
}