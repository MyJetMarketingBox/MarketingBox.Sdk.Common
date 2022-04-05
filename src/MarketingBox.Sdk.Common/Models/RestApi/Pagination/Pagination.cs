namespace MarketingBox.Sdk.Common.Models.RestApi.Pagination
{
    public class Pagination<T>
    {
        public T Cursor { get; set; }
        public int Count { get; set; }
        
        public int Total { get; set; }
        public PaginationOrder Order { get; set; }
        public string NextUrl { get; set; }
    }
}
