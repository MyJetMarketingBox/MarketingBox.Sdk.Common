using System;
using MarketingBox.Sdk.Common.Models;
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
                controllerBase.ModelState.AddModelError("Error", result.ErrorMessage);
                return controllerBase.NotFound(controllerBase.ModelState);
            case ResponseStatus.BadRequest:
                controllerBase.ModelState.AddModelError("Error", result.ErrorMessage);
                return controllerBase.BadRequest(controllerBase.ModelState);
            case ResponseStatus.InternalError:
                return controllerBase.StatusCode(StatusCodes.Status500InternalServerError);
            case ResponseStatus.Unauthorized:
                return controllerBase.StatusCode(StatusCodes.Status401Unauthorized);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}