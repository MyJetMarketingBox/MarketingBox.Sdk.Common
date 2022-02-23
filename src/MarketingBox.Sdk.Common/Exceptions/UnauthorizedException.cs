using System;

namespace MarketingBox.Sdk.Common.Exceptions;

public class UnauthorizedException : Exception
{
    private const string DefaultExceptionFormat = "Unauthorized access attempt for user with id:{0}."; 
    private const string CustomExceptionFormat = "Unauthorized access attempt for {0} with id:{1}."; 
    public UnauthorizedException(long userId): base (string.Format(DefaultExceptionFormat,userId))
    { } 
    public UnauthorizedException(string userName,long userId): base (string.Format(CustomExceptionFormat,userName,userId))
    { }
    public UnauthorizedException(string message): base (message)
    { }
    public UnauthorizedException()
    { }
}