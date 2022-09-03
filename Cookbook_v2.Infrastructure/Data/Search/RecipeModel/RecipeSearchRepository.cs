using System.Linq;
using System.Threading.Tasks;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Cookbook_v2.Domain.Entities.TagModel;
using Cookbook_v2.Domain.Search.Interfaces;
using Cookbook_v2.Domain.Search.RecipeModel;
using Microsoft.EntityFrameworkCore;

namespace Cookbook_v2.Infrastructure.Data.Search.RecipeModel
{
    public class RecipeSearchRepository : IRecipeSearchRepository
    {
        private readonly CookbookContext _context;

        public RecipeSearchRepository( CookbookContext context )
        {
            _context = context;
        }

        public async Task<RecipeSearchResult> Search( RecipeSearchFilters searchFilters )
        {
            IQueryable<Recipe> query = _context.Recipes;

            if ( searchFilters.UserId != null )
            {
                query = query.Where( x => x.UserId == searchFilters.UserId );
            }

            if ( searchFilters.Tags != null && searchFilters.Tags.Any() )
            {
                query = query.Where( x => x.Tags.Select( x => x.Name )
                    .Any( t => searchFilters.Tags.Contains( t ) ) );
            }

            return new RecipeSearchResult
            {
                Result = await query
                    .Include( x => x.IngredientsSections )
                    .Include( x => x.RecipeSteps )
                    .Include( x => x.Tags )
                    .AsSplitQuery().ToListAsync()
            };
        }
    }
}
