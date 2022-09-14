using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cookbook_v2.Domain.Repositories.Interfaces;
using Cookbook_v2.Domain.Entities.RecipeModel;
using System.Linq;
using Cookbook_v2.Domain.Entities.UserModel;
using Cookbook_v2.Domain.EntitiesValidators;

namespace Cookbook_v2.Infrastructure.Data.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly CookbookContext _context;

        public RecipeRepository( CookbookContext context )
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Recipe>> GetAll()
        {
            IReadOnlyList<Recipe> recipes = await _context.Recipes
                .Include( x => x.IngredientsSections )
                .Include( x => x.RecipeSteps )
                .Include( x => x.Tags )
                .AsSplitQuery().ToListAsync();

            return recipes;
        }

        public async Task<Recipe> GetById( int id )
        {
            Recipe recipe = await _context.Recipes
                .Include( x => x.IngredientsSections )
                .Include( x => x.RecipeSteps )
                .Include( x => x.Tags )
                .AsSplitQuery()
                .SingleOrDefaultAsync( x => x.Id == id );

            return recipe;
        }

        public async Task<IReadOnlyList<Recipe>> GetByUserId( int id )
        {
            User user = await _context.Users
                .Include( x => x.Recipes ).ThenInclude( x => x.RecipeSteps )
                .Include( x => x.Recipes ).ThenInclude( x => x.IngredientsSections )
                .Include( x => x.Recipes ).ThenInclude( x => x.Tags )
                .AsSplitQuery().SingleOrDefaultAsync( x => x.Id == id );
            user.ThrowNotFoundIfNull( "User not found" );
            IReadOnlyList<Recipe> recipes = user.Recipes;

            return recipes;
        }

        public async Task<IReadOnlyList<Recipe>> GetFavoritesByUserId( int id )
        {
            IReadOnlyList<Recipe> recipes = await _context.FavoriteRecipes
                .Where( x => x.UserId == id )
                .Join(
                _context.Recipes
                    .Include( x => x.IngredientsSections )
                    .Include( x => x.RecipeSteps )
                    .Include( x => x.Tags ).AsSplitQuery(),
                f => f.RecipeId,
                r => r.Id,
                ( f, r ) => r ).ToListAsync();

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

        public Task Update( Recipe recipe )
        {
            _context.Recipes.Update( recipe );

            return Task.CompletedTask;
        }

        public async Task AddRecipeLike( RecipeLike recipeLike )
        {
            await _context.RecipeLikes.AddAsync( recipeLike );
        }

        public Task DeleteRecipeLike( RecipeLike recipeLike )
        {
            _context.RecipeLikes.Remove( recipeLike );

            return Task.CompletedTask;
        }

        public async Task<RecipeLike> GetRecipeLike( int userId, int recipeId )
        {
            return await _context.RecipeLikes
                .SingleOrDefaultAsync( x => x.UserId == userId && x.RecipeId == recipeId );
        }

        public async Task AddFavoriteRecipe( FavoriteRecipe favoriteRecipe )
        {
            await _context.FavoriteRecipes.AddAsync( favoriteRecipe );
        }

        public Task RemoveFavoriteRecipe( FavoriteRecipe favoriteRecipe )
        {
            _context.FavoriteRecipes.Remove( favoriteRecipe );
            return Task.CompletedTask;
        }

        public async Task<FavoriteRecipe> GetFavoriteRecipe( int userId, int recipeId )
        {
            return await _context.FavoriteRecipes
                .SingleOrDefaultAsync( x => x.UserId == userId && x.RecipeId == recipeId );
        }
    }
}
