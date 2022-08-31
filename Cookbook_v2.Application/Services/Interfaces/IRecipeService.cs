using Cookbook_v2.Application.Commands.RecipeModel;
using Cookbook_v2.Application.Dtos.RecipeModel;
using Cookbook_v2.Domain.Entities.RecipeModel;

namespace Cookbook_v2.Application.Services.Interfaces
{
    public interface IRecipeService : IService<Recipe>
    {
        Task<IReadOnlyList<Recipe>> GetAll();
        Task<Recipe> GetById( int id );
        Task<IReadOnlyList<Recipe>> GetByUserId( int id );
        Task<RecipeDetailsDto> GetRecipeDetailsDtoById( int id );
        Task<Recipe> Create( CreateRecipeCommand createCommand );
        Task DeleteById( int id );
        Task Delete( Recipe recipe );
        Task AddLike( int userId, int recipeId );
        Task DeleteLike( int userId, int recipeID );
    }
}
