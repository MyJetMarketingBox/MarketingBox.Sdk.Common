using System;

namespace MarketingBox.Sdk.Common.Exceptions;

public class AlreadyExistsException : Exception
{
    private const string ExceptionFormat = "{0}:{1} already exists.";
    public AlreadyExistsException(string entityName, object value) : base(string.Format(ExceptionFormat, entityName, value))
    { }

    public AlreadyExistsException(string message): base(message)
    { }
}