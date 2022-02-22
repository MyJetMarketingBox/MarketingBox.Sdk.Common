using System.Collections.Generic;
using MarketingBox.Sdk.Common.Models.RestApi.Pagination;

namespace MarketingBox.Sdk.Common.Models.RestApi;

public class Paginated<TItem, TId>
{
    public Pagination<TId> Pagination { get; set; }
    public IReadOnlyCollection<TItem> Items { get; set; }
}