using System.Runtime.Serialization;

namespace MarketingBox.Sdk.Common.Models.Grpc
{
    [DataContract]
    public class Response<TData>
    {
        [DataMember(Order = 1)]
        public ResponseStatus Status { get; set; }
        
        [DataMember(Order = 2)]
        public Error Error { get; set; }

        [DataMember(Order = 3)]
        public TData Data { get; set; }
    }
}