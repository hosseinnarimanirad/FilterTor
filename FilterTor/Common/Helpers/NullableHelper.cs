namespace FilterTor.Helpers;

using System;
using System.Collections.Generic;
using System.Text;

public static class NullableHelper
{
    public static bool? TryParseBool(string value)
    {
        if (bool.TryParse(value, out bool result))
            return result;
        else
            return null;
    }

    public static byte? TryParseByte(string value)
    {
        if (byte.TryParse(value, out byte result))
            return result;
        else
            return null;
    }

    public static int? TryParseInt(string value)
    {
        if (int.TryParse(value, out int result))
            return result;
        else
            return null;
    }

    public static double? TryParseDouble(string value)
    {
        if (double.TryParse(value, out double result))
            return result;
        else
            return null;
    }

    public static decimal? TryParseDecimal(string value)
    {
        if (decimal.TryParse(value, out decimal result))
            return result;
        else
            return null;
    }

    public static DateTime? TryParseDateTime(string value)
    {
        if (DateTime.TryParse(value, out DateTime result))
            return result;
        else
            return null;
    }
}
