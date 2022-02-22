using AutoWrapper.Wrappers;
using MarketingBox.Sdk.Common.Models.Grpc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketingBox.Sdk.Common.Extensions;

public static class ControllerExtensions
{
    public static ActionResult<TOut> ProcessResult<TOut,TIn>(
        this ControllerBase controllerBase,
        Response<TIn> result,
        TOut body)
    {
        switch (result.Status)
        {
            case ResponseStatus.Ok:
                return controllerBase.Ok(body);
            case ResponseStatus.NotFound:
                throw new ApiException(result.Error,StatusCodes.Status404NotFound);
            case ResponseStatus.BadRequest:
                throw new ApiException(result.Error);
            case ResponseStatus.Unauthorized:
                throw new ApiException(result.Error,StatusCodes.Status401Unauthorized);
            case ResponseStatus.Forbidden:
                throw new ApiException(result.Error,StatusCodes.Status403Forbidden);
            case ResponseStatus.InternalError:
            default:
                throw new ApiException(result.Error, StatusCodes.Status500InternalServerError);
        }
    }
}