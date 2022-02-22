using System;
using MarketingBox.Sdk.Common.Models.RestApi;
using MarketingBox.Sdk.Common.Models.RestApi.Pagination;

namespace MarketingBox.Sdk.Common.Extensions
{
    public static class PaginatedExtensions
    {
        public static Paginated<TItem, TId> Empty<TItem, TId>(PaginationRequest<TId> request)
        {
            return new Paginated<TItem, TId>
            {
                Pagination = new Pagination<TId>
                {
                    Order = request.Order,
                    Cursor = request.Cursor
                },
                Items = ArraySegment<TItem>.Empty
            };
        }
    }
}
