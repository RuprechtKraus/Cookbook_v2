using System.Collections.Generic;

namespace Cookbook_v2.Domain.Search
{
    public class SearchResult<TItem>
    {
        public IEnumerable<TItem> Result { get; set; } = new List<TItem>();
    }
}
