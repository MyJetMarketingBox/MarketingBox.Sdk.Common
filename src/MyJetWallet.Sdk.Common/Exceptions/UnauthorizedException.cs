using System;

namespace MyJetWallet.Sdk.Common.Exceptions;

public class UnauthorizedException : Exception
{
    private const string ExceptionFormat = "Unauthorized access attempt for user with id:{0}."; 
    public UnauthorizedException(long userId): base (string.Format(ExceptionFormat,userId))
    { }
    public UnauthorizedException(string message): base (message)
    { }
}