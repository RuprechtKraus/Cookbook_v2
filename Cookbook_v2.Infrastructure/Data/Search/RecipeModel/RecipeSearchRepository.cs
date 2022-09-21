using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Cookbook_v2.Domain.Search.Interfaces;
using Cookbook_v2.Domain.Search.RecipeModel;
using LinqKit;
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
            IQueryable<Recipe> query = _context.Recipes.AsExpandable();

            if ( !string.IsNullOrWhiteSpace( searchFilters.SearchString ) )
            {
                IEnumerable<string> keywords = searchFilters.SearchString
                    .Split( ',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries );

                ExpressionStarter<Recipe> predicate = PredicateBuilder.New<Recipe>()
                    .Or( x => x.Tags.Select( x => x.Name ).Any( t => keywords.Contains( t ) ) );

                foreach ( string word in keywords )
                {
                    predicate = predicate.Or( x => x.Title.Contains( word ) );
                }

                query = query.Where( predicate );
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
