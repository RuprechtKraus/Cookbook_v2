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
        private readonly DbSet<Recipe> _recipes;
        private readonly DbSet<User> _users;
        private readonly DbSet<RecipeLike> _recipeLikes;
        private readonly DbSet<FavoriteRecipe> _favoriteRecipes;

        public RecipeRepository( CookbookContext context )
        {
            _context = context;
            _recipes = _context.Set<Recipe>();
            _users = _context.Set<User>();
            _recipeLikes = _context.Set<RecipeLike>();
            _favoriteRecipes = _context.Set<FavoriteRecipe>();
        }

        public async Task<IReadOnlyList<Recipe>> GetAll()
        {
            IReadOnlyList<Recipe> recipes = await _recipes
                .Include( x => x.IngredientsSections )
                .Include( x => x.RecipeSteps )
                .Include( x => x.Tags )
                .AsSplitQuery().ToListAsync();

            return recipes;
        }

        public async Task<Recipe> GetById( int id )
        {
            Recipe recipe = await _recipes
                .Include( x => x.IngredientsSections )
                .Include( x => x.RecipeSteps )
                .Include( x => x.Tags )
                .AsSplitQuery()
                .SingleOrDefaultAsync( x => x.Id == id );

            return recipe;
        }

        public async Task<IReadOnlyList<Recipe>> GetByUserId( int id )
        {
            User user = await _users
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
            IReadOnlyList<Recipe> recipes = await _favoriteRecipes
                .Where( x => x.UserId == id )
                .Join(
                _recipes
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
            await _recipes.AddAsync( recipe );
        }

        public Task Delete( Recipe recipe )
        {
            _recipes.Remove( recipe );

            return Task.CompletedTask;
        }

        public Task Update( Recipe recipe )
        {
            _recipes.Update( recipe );

            return Task.CompletedTask;
        }

        public async Task AddRecipeLike( RecipeLike recipeLike )
        {
            await _recipeLikes.AddAsync( recipeLike );
        }

        public Task DeleteRecipeLike( RecipeLike recipeLike )
        {
            _recipeLikes.Remove( recipeLike );

            return Task.CompletedTask;
        }

        public async Task<RecipeLike> GetRecipeLike( int userId, int recipeId )
        {
            return await _recipeLikes
                .SingleOrDefaultAsync( x => x.UserId == userId && x.RecipeId == recipeId );
        }

        public async Task AddFavoriteRecipe( FavoriteRecipe favoriteRecipe )
        {
            await _favoriteRecipes.AddAsync( favoriteRecipe );
        }

        public Task RemoveFavoriteRecipe( FavoriteRecipe favoriteRecipe )
        {
            _favoriteRecipes.Remove( favoriteRecipe );
            return Task.CompletedTask;
        }

        public async Task<FavoriteRecipe> GetFavoriteRecipe( int userId, int recipeId )
        {
            return await _favoriteRecipes
                .SingleOrDefaultAsync( x => x.UserId == userId && x.RecipeId == recipeId );
        }
    }
}
