using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cookbook_v2.Domain.Entities.RecipeModel;

namespace Cookbook_v2.Domain.Repositories.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<IReadOnlyList<Recipe>> GetAll();
        Task<Recipe> GetById( int id );
        Task<IReadOnlyList<Recipe>> GetByUserId( int id );
        Task Add( Recipe recipe );
        Task Delete( Recipe recipe );
        Task Update( Recipe recipe );
        Task AddRecipeLike( RecipeLike recipeLike );
        Task DeleteRecipeLike( RecipeLike recipeLike );
        Task<RecipeLike> GetRecipeLike( int userId, int recipeId );
        Task AddFavoriteRecipe( FavoriteRecipe favoriteRecipe );
        Task RemoveFavoriteRecipe( FavoriteRecipe favoriteRecipe );
        Task<FavoriteRecipe> GetFavoriteRecipe( int userId, int recipeId );
    }
}
