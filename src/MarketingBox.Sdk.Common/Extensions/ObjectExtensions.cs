using System;

namespace MarketingBox.Sdk.Common.Extensions;

internal static class ObjectExtensions
{
    internal static bool IsNumber(this object value)
    {
        return value is sbyte or byte or short or ushort or int or uint or long or ulong or float or double or decimal;
    }

    internal static TimeSpan? ToTimeSpan(this object value)
    {
        TimeSpan? timeSpan = null;

        if (value == null) return null;
        if (TimeSpan.TryParse(value.ToString(), out var outTimeSpan))
        {
            timeSpan = outTimeSpan;
        }
        else
        {
            throw new FormatException("The provided string is not a valid timespan.");
        }

        return timeSpan;
    }

    internal static DateTime? ToDateTime(this object value)
    {
        DateTime? dateTime = null;

        if (value == null) return null;
        if (DateTime.TryParse(value.ToString(), out var outDateTime))
        {
            dateTime = outDateTime;
        }
        else
        {
            throw new FormatException(
                "The string was not recognized as a valid DateTime. String format should be: 01-Jan-2020 or 01-Jan-2020 10:00:00 AM");
        }

        return dateTime;
    }
}