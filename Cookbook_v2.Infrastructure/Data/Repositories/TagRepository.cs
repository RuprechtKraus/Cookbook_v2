using System.Collections.Generic;
using System.Linq;
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

        public async Task<IReadOnlyList<Tag>> GetAll()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag> GetByName( string name )
        {
            return await _context.Tags
                .SingleOrDefaultAsync( x => x.Name == name );
        }
        
        public async Task<IReadOnlyList<Tag>> GetAllByNames( IReadOnlyList<string> names )
        {
            return await _context.Tags
                .Where( x => names.Contains( x.Name ) ).ToListAsync();
        }
    }
}
