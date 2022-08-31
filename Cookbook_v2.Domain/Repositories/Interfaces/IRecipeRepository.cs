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
        Task AddLike( RecipeLike recipeLike );
        Task DeleteLike( RecipeLike recipeLike );
    }
}
