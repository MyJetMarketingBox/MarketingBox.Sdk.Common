using System.Runtime.Serialization;

namespace MarketingBox.Sdk.Common.Models
{
    [DataContract]
    public class Response<TData>
    {
        [DataMember(Order = 1)]
        public ResponseStatus Status { get; set; }
        
        [DataMember(Order = 2)]
        public string ErrorMessage { get; set; }

        [DataMember(Order = 3)]
        public TData Data { get; set; }
    }
}