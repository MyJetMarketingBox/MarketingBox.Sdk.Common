using System;
using System.Linq;
using FluentValidation;
using MarketingBox.Sdk.Common.Exceptions;
using MarketingBox.Sdk.Common.Models;
using MarketingBox.Sdk.Common.Models.Grpc;

namespace MarketingBox.Sdk.Common.Extensions;

public static class ServiceExtensions
{
    public static Response<T> FailedResponse<T>(this Exception ex)
    {
        var error = new Error
        {
            ErrorMessage = ex.Message
        };
        ResponseStatus status;
        switch (ex)
        {
            case NotFoundException:
                status = ResponseStatus.NotFound;
                break;
            case ValidationException exception:
                error.ErrorMessage = BadRequestException.DefaultErrorMessage;
                error.ValidationErrors = exception.Errors.Select(x => new ValidationError
                {
                    ErrorMessage = x.ErrorMessage,
                    ParameterName = x.PropertyName
                }).ToList();
                status = ResponseStatus.BadRequest;
                break;
            case BadRequestException exception:
                error = exception.Error;
                status = ResponseStatus.BadRequest;
                break;
            case AlreadyExistsException:
                status = ResponseStatus.BadRequest;
                break;
            case ForbiddenException:
                status = ResponseStatus.Forbidden;
                break;
            case UnauthorizedException:
                status = ResponseStatus.Unauthorized;
                break;
            default:
                status = ResponseStatus.InternalError;
                break;
        }

        return new Response<T>
        {
            Status = status,
            Error =  error
        };
    }
    
    public static T Process<T>(this Response<T> response)
    {
        return response.Status switch
        {
            ResponseStatus.Ok => response.Data,
            ResponseStatus.NotFound => throw new NotFoundException(response.Error.ErrorMessage),
            ResponseStatus.BadRequest => throw new BadRequestException(response.Error),
            ResponseStatus.Forbidden => throw new ForbiddenException(response.Error.ErrorMessage),
            ResponseStatus.Unauthorized => throw new UnauthorizedException(response.Error.ErrorMessage),
            _ => throw new Exception(response.Error.ErrorMessage)
        };
    }
}