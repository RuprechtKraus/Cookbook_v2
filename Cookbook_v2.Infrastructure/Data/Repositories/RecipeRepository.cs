using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cookbook_v2.Domain.Repositories.Interfaces;
using Cookbook_v2.Domain.Entities.RecipeModel;

namespace Cookbook_v2.Infrastructure.Data.Repositories
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

        public async Task Add( Recipe recipe )
        {
            await _context.Recipes.AddAsync( recipe );
        }

        public Task Delete( Recipe recipe )
        {
            _context.Recipes.Remove( recipe );
            return Task.CompletedTask;
        }

        public async Task AddLike( RecipeLike recipeLike )
        {
            await _context.RecipeLikes.AddAsync( recipeLike );
        }

        public Task DeleteLike( RecipeLike recipeLike )
        {
            _context.RecipeLikes.Remove( recipeLike );
            return Task.CompletedTask;
        }
    }
}
