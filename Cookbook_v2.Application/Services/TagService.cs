using Cookbook_v2.Application.Services.Interfaces;
using Cookbook_v2.Domain.Entities.TagModel;
using Cookbook_v2.Domain.Repositories.Interfaces;

namespace Cookbook_v2.Application.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService( ITagRepository tagRepository )
        {
            _tagRepository = tagRepository;
        }

        public async Task<IReadOnlyList<Tag>> GetAll()
        {
            return await _tagRepository.GetAll();
        }

        public async Task<Tag> GetByName( string name )
        {
            return await _tagRepository.GetByName( name );
        }

        public async Task<Tag> GetByNameOrThrow( string name )
        {
            Tag tag = await _tagRepository.GetByName( name );
            return tag ?? throw new KeyNotFoundException( "Tag not found" );
        }
    }
}
