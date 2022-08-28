using System.Collections.Generic;

namespace Cookbook_v2.Domain.Entities.RecipeModel
{
    public class Recipe : EntityBase
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TimesLiked { get; set; }
        public int TimesFavorited { get; set; }
        public int CookingTimeInMinutes { get; set; }
        public int ServingsCount { get; set; }
        public string ImageName { get; set; }
        public string Tags { get; set; }
        public virtual List<RecipeStep> RecipeSteps { get; set; }
        public virtual List<RecipeIngredientsSection> IngredientsSections { get; set; }

        // Workaround for EF
        protected Recipe()
        {
        }
    }
}
