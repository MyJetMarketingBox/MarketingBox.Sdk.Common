using MarketingBox.Sdk.Common.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace MarketingBox.Sdk.Common.Extensions;

public static class MiddleWareExtensions
{
    public static IApplicationBuilder UseExceptions(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleWare>();
    }
}