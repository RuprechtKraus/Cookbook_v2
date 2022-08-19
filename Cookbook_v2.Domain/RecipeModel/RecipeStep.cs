using System;
using Cookbook_v2.Toolkit.Domain.Abstractions;

namespace Cookbook_v2.Domain.RecipeModel
{
    public class RecipeStep : Entity
    {
        public static readonly int s_descriptionMaxLength = 500;
        public int RecipeId { get; set; }
        public int Index { get; set; }
        public string Description { get; set; }

        protected RecipeStep()
        {
        }
    }
}
