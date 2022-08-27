using Cookbook_v2.Domain.Entities.Interfaces;

namespace Cookbook_v2.Domain.Entities
{
    public class EntityBase : IEntity
    {
        public int Id { get; set; }
    }
}
