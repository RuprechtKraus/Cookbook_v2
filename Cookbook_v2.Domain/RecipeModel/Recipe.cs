using System.Collections.Generic;
using Cookbook_v2.Toolkit.Domain.Abstractions;

namespace Cookbook_v2.Domain.RecipeModel
{
    public class Recipe : Entity
    {
        public static readonly int s_titleMaxLength = 50;
        public static readonly int s_descriptionMaxLength = 1000;
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TimesLiked { get; set; }
        public int TimesFavorited { get; set; }
        public int CookingTimeInMinutes { get; set; }
        public int ServingsCount { get; set; }
        public string ImageUrl { get; set; }
        public string Tags { get; set; }
        public virtual List<RecipeStep> RecipeSteps { get; set; }
        public virtual List<RecipeIngredientsSection> IngredientsSections { get; set; }
    }
}
