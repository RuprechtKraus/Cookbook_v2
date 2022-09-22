using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cookbook_v2.Domain.CategoryModel;
using Cookbook_v2.Domain.Repositories.Interfaces;

namespace Cookbook_v2.Infrastructure.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CookbookContext _context;
        private readonly DbSet<Category> _categories;

        public CategoryRepository( CookbookContext context )
        {
            _context = context;
            _categories = _context.Set<Category>();
        }

        public async Task<IReadOnlyList<Category>> GetAll()
        {
            return await _categories.ToListAsync();
        }
    }
}
