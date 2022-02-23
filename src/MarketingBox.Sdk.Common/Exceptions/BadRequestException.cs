using System;
using MarketingBox.Sdk.Common.Models;

namespace MarketingBox.Sdk.Common.Exceptions;

public class BadRequestException : Exception
{
    public const string DefaultErrorMessage = "Request responded with one or more validation errors.";
    public Error Error { get; set; }

    public BadRequestException(string message) : base(message)
    {
        Error = new Error
        {
            ErrorMessage = message
        };
    }

    public BadRequestException(Error error) : base(error.ErrorMessage)
    {
        Error = error;
    }
}