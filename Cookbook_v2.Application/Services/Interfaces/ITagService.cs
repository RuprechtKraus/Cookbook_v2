using Cookbook_v2.Domain.Entities.TagModel;

namespace Cookbook_v2.Application.Services.Interfaces
{
    public interface ITagService : IService<Tag>
    {
        Task<IReadOnlyList<Tag>> GetAll();
        Task<Tag> GetByName( string name );
        Task<Tag> GetByNameOrThrow( string name );
    }
}
