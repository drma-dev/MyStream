﻿using System;
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
                output.Add(new EnumList()
                {
                    Group = GetGroup((Enum)val),
                    Value = (int)val,
                    ValueObject = val,
                    Name = GetName((Enum)val),
                    Description = GetDescription((Enum)val),
                });
            }

            return output;
        }

        public static string GetName(this Enum value)
        {
            if (value == null) return null;

            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo == null) return null;

            var result = ((DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false))[0].Name;
            var type = ((DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false))[0].ResourceType;

            if (result != null && type != null)
            {
                var rm = new ResourceManager(type.FullName, type.Assembly);
                result = rm.GetString(result);
            }

            return result;
        }

        public static string GetDescription(this Enum value)
        {
            if (value == null) return null;

            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo == null) return null;

            var result = ((DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false))[0].Description;
            var type = ((DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false))[0].ResourceType;

            if (result != null && type != null)
            {
                var rm = new ResourceManager(type.FullName, type.Assembly);
                result = rm.GetString(result);
            }

            return result;
        }

        public static string GetGroup(this Enum value)
        {
            if (value == null) return null;

            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo == null) return null;

            return ((DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false))[0].GroupName;
        }
    }

    public class EnumList
    {
        public string Group { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public object ValueObject { get; set; }
    }
}