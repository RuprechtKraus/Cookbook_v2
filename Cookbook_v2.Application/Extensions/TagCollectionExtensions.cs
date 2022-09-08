using Cookbook_v2.Application.Helpers.Comparators;
using Cookbook_v2.Domain.Entities.TagModel;

namespace Cookbook_v2.Application.Extensions
{
    public static class TagCollectionExtensions
    {
        public static IEnumerable<Tag> Intersect( this ICollection<Tag> left, ICollection<Tag> right )
        {
            return left.Intersect( right, new TagEqualityComparer() );
        }

        public static IEnumerable<Tag> Except( this ICollection<Tag> left, ICollection<Tag> right )
        {
            return left.Except( right, new TagEqualityComparer() );
        }
    }
}
