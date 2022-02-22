using System.Collections.Generic;
using System.Runtime.Serialization;
using AutoWrapper;

namespace MarketingBox.Sdk.Common.Models;


[DataContract]
public class Error
{
    [DataMember(Order = 1)]
    [AutoWrapperPropertyMap(Prop.ResponseException_ExceptionMessage)]
    public string ErrorMessage { get; set; }
    
    [DataMember(Order = 2)]
    [AutoWrapperPropertyMap(Prop.ResponseException_ValidationErrors)]
    public List<ValidationError> ValidationErrors { get; set; }
}

[DataContract]
public class ValidationError
{
    [DataMember(Order = 1)]
    [AutoWrapperPropertyMap(Prop.ResponseException_ValidationErrors_Field)]
    public string ParameterName { get; set; }
    
    [DataMember(Order = 2)]
    [AutoWrapperPropertyMap(Prop.ResponseException_ValidationErrors_Message)]
    public string ErrorMessage { get; set; }
}