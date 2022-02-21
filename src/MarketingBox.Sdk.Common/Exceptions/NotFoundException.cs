using System;

namespace MarketingBox.Sdk.Common.Exceptions;

public class NotFoundException : Exception
{
    const string ExceptionFormat = "{0}:{1} was not found.";
    public NotFoundException(string entityName, object value): base(string.Format(ExceptionFormat, entityName, value))
    { }

    public NotFoundException(string message) : base(message)
    { }
}