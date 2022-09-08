using Cookbook_v2.Domain.Entities.TagModel;

namespace Cookbook_v2.Application.Helpers.Comparators
{
    class TagEqualityComparer : IEqualityComparer<Tag>
    {
        public bool Equals( Tag? t1, Tag? t2 )
        {
            if ( t1 == null && t2 == null )
            {
                return true;
            }
            if ( t1 == null || t2 == null )
            {
                return false;
            }
            return t1.Name == t2.Name;
        }

        public int GetHashCode( Tag tag )
        {
            return tag.Name.GetHashCode();
        }
    }
}
