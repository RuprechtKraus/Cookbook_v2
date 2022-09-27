using Cookbook_v2.Application.Commands.RecipeModel;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Cookbook_v2.Domain.Search.RecipeModel;

namespace Cookbook_v2.Application.Services.Interfaces
{
    public interface IRecipeService : IService<Recipe>
    {
        Task<IReadOnlyList<Recipe>> GetAll();
        Task<Recipe> GetById( int id );
        Task<IReadOnlyList<Recipe>> GetByUserId( int id );
        Task<IReadOnlyList<Recipe>> GetFavoritesByUserId( int id );
        Task<RecipeSearchResult> Search( RecipeSearchFilters searchFilters );
        Task<Recipe> Create( CreateRecipeCommand createCommand );
        Task Update( Recipe recipe, UpdateRecipeCommand updateCommand );
        Task DeleteById( int id );
        Task Delete( Recipe recipe );
        Task<RecipeLike> AddUserLike( int userId, int recipeId );
        Task DeleteUserLike( int userId, int recipeId );
        Task<bool> HasUserLike( int userId, int recipeId );
        Task<int> GetLikeCount( int id );
        Task<FavoriteRecipe> AddToUserFavorites( int userId, int recipeId );
        Task RemoveFromUserFavorites( int userId, int recipeId );
        Task<bool> IsFavoritedByUser( int userId, int recipeId );
        Task<int> GetFavoriteCount( int id );
    }
}
