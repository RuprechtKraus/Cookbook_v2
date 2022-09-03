using System.Collections.Generic;
using System.Threading.Tasks;
using Cookbook_v2.Domain.Entities.TagModel;

namespace Cookbook_v2.Domain.Repositories.Interfaces
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<IReadOnlyList<Tag>> GetAll();
        Task<Tag> GetByName( string name );
        Task<IReadOnlyList<Tag>> GetAllByNames( IReadOnlyList<string> names );
    }
}
