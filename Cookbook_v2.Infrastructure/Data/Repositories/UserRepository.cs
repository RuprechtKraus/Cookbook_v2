using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cookbook_v2.Domain.Repositories.Interfaces;
using Cookbook_v2.Domain.Entities.UserModel;
using Cookbook_v2.Domain.Entities.RecipeModel;

namespace Cookbook_v2.Infrastructure.Data.Repositories
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

        public async Task AddFavoriteRecipe( FavoriteRecipe favoriteRecipe )
        {
            await _context.FavoriteRecipes.AddAsync( favoriteRecipe );
        }

        public Task RemoveFavoriteRecipe( FavoriteRecipe favoriteRecipe )
        {
            _context.FavoriteRecipes.Remove( favoriteRecipe );
            return Task.CompletedTask;
        }
    }
}
