using AutoWrapper;

namespace MarketingBox.Sdk.Common.Models.RestApi;

public class ApiResponseMap
{
    [AutoWrapperPropertyMap(Prop.Result)] public object Data { get; set; }

    [AutoWrapperPropertyMap(Prop.ResponseException)] public Error Error { get; set; }
}