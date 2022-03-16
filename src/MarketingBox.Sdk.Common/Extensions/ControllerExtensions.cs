using System;
using System.Linq;
using AutoWrapper.Wrappers;
using FluentValidation;
using MarketingBox.Sdk.Common.Exceptions;
using MarketingBox.Sdk.Common.Models;
using MarketingBox.Sdk.Common.Models.Grpc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketingBox.Sdk.Common.Extensions;

public static class ControllerExtensions
{
    public static ActionResult<TOut> ProcessResult<TOut, TIn>(
        this ControllerBase controllerBase,
        Response<TIn> result,
        TOut body)
    {
        switch (result.Status)
        {
            case ResponseStatus.Ok:
                return controllerBase.Ok(body);
            case ResponseStatus.NotFound:
                throw new ApiException(result.Error, StatusCodes.Status404NotFound);
            case ResponseStatus.BadRequest:
                throw new ApiException(result.Error);
            case ResponseStatus.Unauthorized:
                throw new ApiException(result.Error, StatusCodes.Status401Unauthorized);
            case ResponseStatus.Forbidden:
                throw new ApiException(result.Error, StatusCodes.Status403Forbidden);
            case ResponseStatus.InternalError:
            default:
                throw new ApiException(result.Error, StatusCodes.Status500InternalServerError);
        }
    }

    public static IActionResult ProcessResult<TIn>(
        this ControllerBase controllerBase,
        Response<TIn> result)
    {
        switch (result.Status)
        {
            case ResponseStatus.Ok:
                return controllerBase.Ok();
            case ResponseStatus.NotFound:
                throw new ApiException(result.Error, StatusCodes.Status404NotFound);
            case ResponseStatus.BadRequest:
                throw new ApiException(result.Error);
            case ResponseStatus.Unauthorized:
                throw new ApiException(result.Error, StatusCodes.Status401Unauthorized);
            case ResponseStatus.Forbidden:
                throw new ApiException(result.Error, StatusCodes.Status403Forbidden);
            case ResponseStatus.InternalError:
            default:
                throw new ApiException(result.Error, StatusCodes.Status500InternalServerError);
        }
    }

    public static ActionResult<TOut> FailedResponse<TOut>(
        this Exception exception)
    {
        var error = new Error
        {
            ErrorMessage = exception.Message
        };
        switch (exception)
        {
            case NotFoundException:
                throw new ApiException(error, StatusCodes.Status404NotFound);
            case ValidationException ex:
                error.ErrorMessage = BadRequestException.DefaultErrorMessage;
                error.ValidationErrors = ex.Errors.Select(x => new Models.ValidationError
                {
                    ErrorMessage = x.ErrorMessage,
                    ParameterName = x.PropertyName
                }).ToList();
                throw new ApiException(error);
            case BadRequestException ex:
                throw new ApiException(ex.Error);
            case AlreadyExistsException:
                throw new ApiException(error);
            case ForbiddenException:
                throw new ApiException(error, StatusCodes.Status403Forbidden);
            case UnauthorizedException:
                throw new ApiException(error, StatusCodes.Status401Unauthorized);
            default:
                throw new ApiException(error, StatusCodes.Status500InternalServerError);
        }
    }

    public static IActionResult FailedResponse(
        this Exception exception)
    {
        var error = new Error
        {
            ErrorMessage = exception.Message
        };
        switch (exception)
        {
            case NotFoundException:
                throw new ApiException(error, StatusCodes.Status404NotFound);
            case ValidationException ex:
                error.ErrorMessage = BadRequestException.DefaultErrorMessage;
                error.ValidationErrors = ex.Errors.Select(x => new Models.ValidationError
                {
                    ErrorMessage = x.ErrorMessage,
                    ParameterName = x.PropertyName
                }).ToList();
                throw new ApiException(error);
            case BadRequestException ex:
                throw new ApiException(ex.Error);
            case AlreadyExistsException:
                throw new ApiException(error);
            case ForbiddenException:
                throw new ApiException(error, StatusCodes.Status403Forbidden);
            case UnauthorizedException:
                throw new ApiException(error, StatusCodes.Status401Unauthorized);
            default:
                throw new ApiException(error, StatusCodes.Status500InternalServerError);
        }
    }
}