using System;
using System.Collections.Generic;

namespace MarketingBox.Sdk.Common.Extensions;

public static class TypeExtensions
{
    private static readonly HashSet<Type> NumericTypes = new()
    {
        typeof(sbyte),
        typeof(byte),
        typeof(short),
        typeof(ushort),
        typeof(int),
        typeof(uint),
        typeof(long),
        typeof(ulong),
        typeof(float),
        typeof(double),
        typeof(decimal)
    };

    public static bool IsNumericType(this Type type)
    {
        return NumericTypes.Contains(type) || NumericTypes.Contains(Nullable.GetUnderlyingType(type));
    }

    public static bool IsDateTimeType(this Type type)
    {
        return type == typeof(DateTime) || type == typeof(DateTime?);
    }

    public static bool IsTimeSpanType(this Type type)
    {
        return type == typeof(TimeSpan) || type == typeof(TimeSpan?);
    }
}