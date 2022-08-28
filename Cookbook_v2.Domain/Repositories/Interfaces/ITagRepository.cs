using System.Threading.Tasks;
using Cookbook_v2.Domain.Entities.TagModel;

namespace Cookbook_v2.Domain.Repositories.Interfaces
{
    public interface ITagRepository : IRepository<Tag>
    {
        public Task<Tag> GetByName( string name );
    }
}
