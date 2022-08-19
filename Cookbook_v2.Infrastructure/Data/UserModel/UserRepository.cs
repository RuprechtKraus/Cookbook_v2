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

        public async Task<int> Add( User user )
        {
            EntityEntry<User> entry = await _context.AddAsync( user );
            return entry.Entity.Id;
        }

        public async Task AddFavoriteRecipe( User user, Recipe recipe )
        {
            await _context.FavoriteRecipes
                .AddAsync( new FavoriteRecipe( user, recipe ) );
        }

        public async Task RemoveFavoriteRecipe( User user, Recipe recipe )
        {
            FavoriteRecipe favRecipe = await _context.FavoriteRecipes
                .SingleOrDefaultAsync( x => x.UserId == user.Id && x.RecipeId == recipe.Id );
            if ( favRecipe != null )
            {
                _context.FavoriteRecipes.Remove( favRecipe );
            }
        }
    }
}
