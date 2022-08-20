using System.Threading.Tasks;
using System.Collections.Generic;
using Cookbook_v2.Toolkit.Domain.Abstractions;

namespace Cookbook_v2.Domain.CategoryModel
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IReadOnlyList<Category>> GetAll();
    }
}
