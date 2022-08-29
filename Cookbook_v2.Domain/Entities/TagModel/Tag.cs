using System.Collections.Generic;
using Cookbook_v2.Domain.Entities.RecipeModel;

namespace Cookbook_v2.Domain.Entities.TagModel
{
    public class Tag : EntityBase
    {
        public string Name { get; set; }
        public virtual List<Recipe> Recipes { get; set; }

        public Tag( string name )
        {
            Name = name;
        }

        // Workaround for EF
        protected Tag()
        {
        }
    }
}
