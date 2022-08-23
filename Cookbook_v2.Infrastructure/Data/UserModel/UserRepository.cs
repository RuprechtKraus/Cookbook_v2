using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Cookbook_v2.Domain.UserModel;
using Cookbook_v2.Domain.RecipeModel;

namespace Cookbook_v2.Infrastructure.Data.UserModel
{
    public class UserRepository : IUserRepository
    {
        private readonly CookbookContext _context;

        public UserRepository( CookbookContext context )
        {
            _context = context;
        }

        public async Task<User> GetById( int id )
        {
            User user = await _context.Users
                .SingleOrDefaultAsync( x => x.Id == id );
            return user;
        }

        public async Task<User> GetByUsername( string username )
        {
            User user = await _context.Users
                .SingleOrDefaultAsync( x => x.Username == username );
            return user;
        }

        public async Task Add( User user )
        {
            await _context.Users.AddAsync( user );
        }

        public async Task AddFavoriteRecipe( FavoriteRecipe favRecipe )
        {
            await _context.FavoriteRecipes
                .AddAsync( favRecipe );
        }

        public async Task RemoveFavoriteRecipe( FavoriteRecipe favRecipe )
        {
            FavoriteRecipe favRecipeToDelete = await _context.FavoriteRecipes
                .SingleOrDefaultAsync( x => x.UserId == favRecipe.UserId && x.RecipeId == favRecipe.RecipeId );
            if ( favRecipe != null )
            {
                _context.FavoriteRecipes.Remove( favRecipeToDelete );
            }
        }
    }
}
