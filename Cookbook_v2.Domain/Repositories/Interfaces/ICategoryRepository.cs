using System.Threading.Tasks;
using System.Collections.Generic;
using Cookbook_v2.Domain.CategoryModel;

namespace Cookbook_v2.Domain.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IReadOnlyList<Category>> GetAll();
    }
}
