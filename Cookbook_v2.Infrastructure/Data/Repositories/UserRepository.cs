using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cookbook_v2.Domain.Repositories.Interfaces;
using Cookbook_v2.Domain.Entities.UserModel;

namespace Cookbook_v2.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CookbookContext _context;
        private readonly DbSet<User> _users;

        public UserRepository( CookbookContext context )
        {
            _context = context;
            _users = _context.Set<User>();
        }

        public async Task<User> GetById( int id )
        {
            User user = await _users
                .SingleOrDefaultAsync( x => x.Id == id );
            return user;
        }

        public async Task<User> GetByUsername( string username )
        {
            User user = await _users
                .SingleOrDefaultAsync( x => x.Username == username );
            return user;
        }

        public async Task Add( User user )
        {
            await _users.AddAsync( user );
        }

        public Task Update( User user )
        {
            _users.Update( user );
            return Task.CompletedTask;
        }
    }
}
