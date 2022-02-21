using System;

namespace MarketingBox.Sdk.Common.Exceptions;

public class BadRequestException: Exception
{
    public BadRequestException(string message) : base(message)
    { }
}