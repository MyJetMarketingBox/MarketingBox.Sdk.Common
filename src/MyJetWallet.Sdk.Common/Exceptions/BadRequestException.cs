using System;

namespace MyJetWallet.Sdk.Common.Exceptions;

public class BadRequestException: Exception
{
    public BadRequestException(string message) : base(message)
    { }
}