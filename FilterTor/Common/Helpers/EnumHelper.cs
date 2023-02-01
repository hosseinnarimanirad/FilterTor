namespace FilterTor.Helpers;

using FilterTor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;


public static class EnumHelper
{
    public static bool Validate(this Enum enumValue, string stringValue)
    {
        //return Enum.GetName(enumValue.GetType(), enumValue) == stringValue;
        return enumValue.GetName() == stringValue;
    }

    public static string GetName(this Enum enumValue)
    {
        return Enum.GetName(enumValue.GetType(), enumValue);
    }

    public static string GetDescription(this Enum enumValue)
    {
        try
        {
            if (enumValue == null) return string.Empty;

            return enumValue.GetType()
              .GetMember(enumValue.ToString())
              ?.First()
              ?.GetCustomAttribute<DescriptionAttribute>()
              ?.Description ?? string.Empty;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }

    public static T GetAttribute<T>(this Enum enumValue) where T : System.Attribute
    {
        try
        {
            return enumValue.GetType()
              .GetMember(enumValue.ToString())
              ?.First()
              ?.GetCustomAttribute<T>() ?? default(T);
        }
        catch (Exception ex)
        {
            return default(T);
        }
    }

    public static List<T> GetEnums<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToList();
    }

    public static List<Enum> GetEnums(Enum value)
    {
        return Enum.GetValues(value.GetType()).Cast<Enum>().ToList();
    }

    internal static T Parse<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value);
    }

    public static T TryParseIgnoreCase<T>(string value) where T : struct
    {
        if (Enum.TryParse<T>(value, ignoreCase: true, out var resultValue))
            return resultValue;

        else return default;

        //return (T)Enum.TryParse<T>(value, ignoreCase: true);
    }

    public static T Parse<T>(Enum @enum)
    {
        return (T)Enum.ToObject(typeof(T), @enum);
    }

    public static bool IsDefined<T>(string value) where T : struct
    {
        if (Enum.TryParse<T>(value, ignoreCase: true, out var resultValue))
            return true;

        else
            return false;
    }

    public static bool IsDefined<T>(Enum @enum) where T : struct
    {
        return Enum.IsDefined(typeof(T), @enum);
    }

    public static List<EnumInfo> Parse<T>() where T : struct, IConvertible
    {
        //var type = typeof(T);
        //return EnumHelper.GetEnums<T>().Select(e => new EnumInfo((int)(object)e, Enum.GetName(type, e), GetDescription(e))).ToList();

        Predicate<T> nullPredicate = null;
        return Parse<T>(nullPredicate);
    }

    public static List<EnumInfo> Parse<T>(Predicate<T>? predicate) where T : struct, IConvertible
    {
        var type = typeof(T);

        return EnumHelper.GetEnums<T>().Where(t => predicate == null || predicate(t))
                                        .Select(e => new EnumInfo((int)(object)e, Enum.GetName(type, e), GetDescription(e)))
                                        .ToList();
    }

    public static string GetDescription(object value)
    {
        return value.GetType()
              .GetMember(value.ToString())
              ?.First()
              ?.GetCustomAttribute<DescriptionAttribute>()
              ?.Description ?? string.Empty;
    }
}

