using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using MarketingBox.Sdk.Common.Exceptions;
using MarketingBox.Sdk.Common.Models;

namespace MarketingBox.Sdk.Common.Extensions
{
    public static class FilterExtensions
    {
        public static List<T> Parse<T>(
            this string stringIds,
            [CallerArgumentExpression("stringIds")]
            string callerExp = "")
        {
            try
            {
                var t = typeof(T);
                var res = new List<T>();
                if (string.IsNullOrEmpty(stringIds))
                {
                    return res;
                }

                var ids = stringIds.Split(',').Distinct().ToList();

                if (t.IsEnum)
                {
                    var names = Enum.GetNames(t);
                    var commonNames = ids.Intersect(names).ToList();
                    if (commonNames.Any())
                    {
                        res.AddRange(commonNames.Select(commonName => (T) Enum.Parse(t, commonName)));
                    }
                    else
                    {
                        res.AddRange(ids
                            .Where(x => int.TryParse(x, out var num) && Enum.IsDefined(t, num))
                            .Select(x => (T) Enum.Parse(t, x)));
                    }

                    if (res.Count < ids.Count)
                    {
                        throw new Exception("Can't parse values");
                    }

                    return res;
                }

                foreach (var id in ids)
                {
                    if (id.TryConvertTo<T>(out var val))
                    {
                        res.Add(val);
                    }
                    else
                    {
                        throw new Exception($"Can't parse value: {id}");
                    }
                }

                return res;
            }
            catch (Exception e)
            {
                throw new BadRequestException(new Error
                {
                    ErrorMessage = "Invalid format",
                    ValidationErrors = new List<ValidationError>
                    {
                        new()
                        {
                            ErrorMessage = e.Message,
                            ParameterName = callerExp.Split(".").Last()
                        }
                    }
                });
            }
        }

        public static bool TryConvertTo<T>(this object value, out T result)
        {
            if (value is T variable)
            {
                result = variable;
                return true;
            }

            try
            {
                if (Nullable.GetUnderlyingType(typeof(T)) != null)
                {
                    result = (T) TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(value);
                }
                else
                {
                    result = (T) Convert.ChangeType(value, typeof(T));
                }

                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }
    }
}