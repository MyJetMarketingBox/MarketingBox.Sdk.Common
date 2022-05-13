using System.ComponentModel;
using MarketingBox.Sdk.Common.Attributes;

namespace MarketingBox.Sdk.Common.Models.RestApi.Pagination
{
    public class PaginationRequest<T>
    {
        [DefaultValue(PaginationOrder.Desc)]
        [IsEnum]
        public PaginationOrder? Order { get; set; } = PaginationOrder.Desc;

        public T Cursor { get; set; }

        [AdvancedCompare(ComparisonType.GreaterThan, 0)]
        public int? Limit { get; set; }
    }
}
