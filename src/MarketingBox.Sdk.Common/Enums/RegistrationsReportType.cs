using System.Runtime.Serialization;

namespace MarketingBox.Sdk.Common.Enums
{
    [DataContract]
    public enum RegistrationsReportType
    {
        Registrations,
        Ftd,
        All
    }
}