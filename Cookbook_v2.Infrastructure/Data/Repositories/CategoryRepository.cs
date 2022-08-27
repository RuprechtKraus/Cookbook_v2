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

        public CategoryRepository( CookbookContext context )
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
