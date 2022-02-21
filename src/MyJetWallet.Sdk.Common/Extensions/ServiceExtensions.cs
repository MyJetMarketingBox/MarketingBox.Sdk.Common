using System;
using FluentValidation;
using MyJetWallet.Sdk.Common.Exceptions;
using MyJetWallet.Sdk.Common.Models;

namespace MyJetWallet.Sdk.Common.Extensions;

public static class ServiceExtensions
{
    public static Response<T> FailedResponse<T>(this Exception ex)
    {
        var message = ex.Message;
        ResponseStatus status;
        switch (ex)
        {
            case NotFoundException:
                status = ResponseStatus.NotFound;
                break;
            case ValidationException exception:
                message = string.Join(",\n", exception.Errors);
                status = ResponseStatus.BadRequest;
                break;
            case BadRequestException:
            case AlreadyExistsException:
                status = ResponseStatus.BadRequest;
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
            ErrorMessage = message
        };
    }
}