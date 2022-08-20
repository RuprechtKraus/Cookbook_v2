using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Cookbook_v2.Domain.RecipeModel;

namespace Cookbook_v2.Infrastructure.Data.RecipeModel
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly CookbookContext _context;

        public RecipeRepository( CookbookContext context )
        {
            _context = context;
        }

        public async Task<Recipe> GetById( int id )
        {
            Recipe recipe = await _context.Recipes
                .SingleOrDefaultAsync( x => x.Id == id );
            return recipe;
        }

        public async Task<IReadOnlyList<Recipe>> GetByUsername( string username )
        {
            IReadOnlyList<Recipe> recipes = ( await _context.Users
                .Include( x => x.Recipes ).ThenInclude( x => x.RecipeSteps )
                .Include( x => x.Recipes ).ThenInclude( x => x.IngredientsSections )
                .SingleOrDefaultAsync( x => x.Username == username ) ).Recipes;
            return recipes;
        }

        public async Task<int> Add( Recipe recipe )
        {
            EntityEntry<Recipe> entity = await _context.Recipes.AddAsync( recipe );
            return entity.Entity.Id;
        }

        public async Task Delete( int id )
        {
            Recipe recipe = await _context.Recipes.SingleOrDefaultAsync( x => x.Id == id );
            if ( recipe != null )
            {
                await Delete( recipe );
            }
        }

        public Task Delete( Recipe recipe )
        {
            _context.Recipes.Remove( recipe );
            return Task.CompletedTask;
        }

        public async Task AddLike( RecipeLike like )
        {
            await _context.RecipeLikes.AddAsync( like );
        }

        public Task DeleteLike( RecipeLike like )
        {
            _context.RecipeLikes.Remove( like );
            return Task.CompletedTask;
        }
    }
}
