using System;
using System.Linq;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using FluentValidation;
using MarketingBox.Sdk.Common.Exceptions;
using MarketingBox.Sdk.Common.Models;
using Microsoft.AspNetCore.Http;

namespace MarketingBox.Sdk.Common.Middlewares;

public class ExceptionMiddleWare
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleWare(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception e)
        {
            var error = new Error
            {
                ErrorMessage = e.Message
            };
            switch (e)
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
                case ApiException:
                    throw;
                default:
                    throw new ApiException(error, StatusCodes.Status500InternalServerError);
            }
        }
    }
}