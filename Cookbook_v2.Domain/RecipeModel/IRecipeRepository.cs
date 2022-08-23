using System.Collections.Generic;
using System.Threading.Tasks;
using Cookbook_v2.Toolkit.Domain.Abstractions;

namespace Cookbook_v2.Domain.RecipeModel
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<Recipe> GetById( int id );
        Task<IReadOnlyList<Recipe>> GetByUsername( string username );
        Task Add( Recipe recipe );
        Task Delete( int id );
        Task Delete( Recipe recipe );
        Task AddLike( RecipeLike recipeLike );
        Task DeleteLike( RecipeLike recipeLike );
    }
}
