using MyStream.Core;
using System;
using System.Collections.Generic;

namespace MyStream.Helper
{
    public static class EnumHelper
    {
        public static TEnum[] GetArray<TEnum>() where TEnum : struct, Enum
        {
            return Enum.GetValues<TEnum>();
        }

        public static IEnumerable<EnumObject> GetList<TEnum>(bool translate = true) where TEnum : struct, Enum
        {
            foreach (var val in GetArray<TEnum>())
            {
                var attr = ((Enum)val).GetCustomAttribute(translate);

                yield return new EnumObject
                {
                    Value = Convert.ToInt32(val),
                    ValueObject = val,
                    Name = attr.Name,
                    Description = attr.Description,
                };
            }
        }
    }

    public class EnumObject
    {
        public int Value { get; set; }
        public object ValueObject { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}