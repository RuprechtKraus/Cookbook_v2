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
        private readonly DbSet<Tag> _tags;

        public TagRepository( CookbookContext context )
        {
            _context = context;
            _tags = _context.Set<Tag>();
        }

        public async Task<IReadOnlyList<Tag>> GetAll()
        {
            return await _tags.ToListAsync();
        }

        public async Task<Tag> GetByName( string name )
        {
            return await _tags
                .SingleOrDefaultAsync( x => x.Name == name );
        }

        public async Task<IReadOnlyList<Tag>> GetAllByNames( IReadOnlyList<string> names )
        {
            return await _tags
                .Where( x => names.Contains( x.Name ) ).ToListAsync();
        }
    }
}
