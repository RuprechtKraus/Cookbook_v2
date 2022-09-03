using Cookbook_v2.Domain.Search.RecipeModel;

namespace Cookbook_v2.Domain.Search.Interfaces
{
    public interface IRecipeSearchRepository :
        ISearchRepository<RecipeSearchFilters, RecipeSearchResult>
    {
    }
}
