using System.Collections.Generic;
using Cookbook_v2.Domain.Entities.TagModel;

namespace Cookbook_v2.Domain.Entities.RecipeModel
{
    public partial class Recipe : EntityBase
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TimesLiked { get; set; }
        public int TimesFavorited { get; set; }
        public int CookingTimeInMinutes { get; set; }
        public int ServingsCount { get; set; }
        public string ImageName { get; set; }
        public virtual List<RecipeIngredientsSection> IngredientsSections { get; set; }
        public virtual List<RecipeStep> RecipeSteps { get; set; }
        public virtual List<Tag> Tags { get; set; }

        public Recipe(
            int userId,
            string title,
            string description,
            int cookingTimeInMinutes,
            int servingsCount,
            string imageName,
            List<RecipeStep> recipeSteps,
            List<RecipeIngredientsSection> ingredientsSections,
            List<Tag> tags )
        {
            UserId = userId;
            Title = title;
            Description = description;
            CookingTimeInMinutes = cookingTimeInMinutes;
            ServingsCount = servingsCount;
            ImageName = imageName;
            RecipeSteps = recipeSteps;
            IngredientsSections = ingredientsSections;
            Tags = tags;
        }

        // Workaround for EF
        protected Recipe()
        {
        }
    }
}
