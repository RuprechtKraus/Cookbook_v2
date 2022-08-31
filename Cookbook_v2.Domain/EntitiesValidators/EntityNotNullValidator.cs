using System.Collections.Generic;
using Cookbook_v2.Domain.Entities.Interfaces;

namespace Cookbook_v2.Domain.EntitiesValidators
{
    public static class EntityNotNullValidator
    {
        public static void ThrowNotFoundIfNull( this IEntity entity, string message = "" )
        {
            if ( entity == null )
            {
                throw new KeyNotFoundException( message );
            }
        }
    }
}
