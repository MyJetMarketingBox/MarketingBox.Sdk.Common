using System;

namespace MyJetWallet.Sdk.Common.Exceptions;

public class UnauthorizedException : Exception
{
    private const string ExceptionMessage = "Unauthorized access attempt."; 
    public UnauthorizedException(): base (ExceptionMessage)
    { }
}