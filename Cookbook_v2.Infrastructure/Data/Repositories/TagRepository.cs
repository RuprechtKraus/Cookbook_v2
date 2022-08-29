using System.Threading.Tasks;
using Cookbook_v2.Domain.Entities.TagModel;
using Cookbook_v2.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cookbook_v2.Infrastructure.Data.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly CookbookContext _context;

        public TagRepository( CookbookContext context )
        {
            _context = context;
        }

        public async Task<Tag> GetByName( string name )
        {
            return await _context.Tags
                .SingleOrDefaultAsync( x => x.Name == name );
        }
    }
}
