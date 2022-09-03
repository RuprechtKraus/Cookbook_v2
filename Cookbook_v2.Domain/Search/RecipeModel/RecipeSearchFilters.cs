using System.Collections.Generic;

namespace Cookbook_v2.Domain.Search.RecipeModel
{
    public class RecipeSearchFilters
    {
        public int? UserId { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
