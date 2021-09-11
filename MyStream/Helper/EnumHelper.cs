using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;

namespace MyStream.Helper
{
    public static class EnumHelper
    {
        public static T[] GetArray<T>() where T : struct, IConvertible
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        }

        public static List<EnumList> GetList(Type enumType)
        {
            if (enumType == null) throw new ArgumentNullException(nameof(enumType));
            if (!enumType.IsEnum) throw new ArgumentException("Type must be an enum type.");

            var items = Enum.GetValues(enumType);

            var output = new List<EnumList>();
            foreach (object val in items)
            {
                var attr = ((Enum)val).GetDisplayAttribute();

                output.Add(new EnumList()
                {
                    Value = (int)val,
                    ValueObject = val,
                    Name = ((Enum)val).GetName(attr),
                    Description = ((Enum)val).GetDescription(attr),
                });
            }

            return output;
        }

        public static DisplayAttribute GetDisplayAttribute(this Enum value)
        {
            if (value == null) return null;

            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo == null) return null;

            return ((DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false))[0];
        }

        public static string GetName(this Enum value, DisplayAttribute attr = null)
        {
            if (value == null) return null;

            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo == null) return null;

            if (attr == null) attr = value.GetDisplayAttribute();

            var name = attr.Name;
            var type = attr.ResourceType;

            if (name != null && type != null)
            {
                var rm = new ResourceManager(type.FullName, type.Assembly);
                name = rm.GetString(name);
            }

            return name;
        }

        public static string GetDescription(this Enum value, DisplayAttribute attr = null)
        {
            if (value == null) return null;

            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo == null) return null;

            if (attr == null) attr = value.GetDisplayAttribute();

            var description = attr.Description;
            var type = attr.ResourceType;

            if (description != null && type != null)
            {
                var rm = new ResourceManager(type.FullName, type.Assembly);
                description = rm.GetString(description);
            }

            return description;
        }
    }

    public class EnumList
    {
        public int Value { get; set; }
        public object ValueObject { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}