using Cookbook_v2.Application.Dtos.RecipeModel;
using Cookbook_v2.Domain.Entities.RecipeModel;

namespace Cookbook_v2.Application.Converters
{
    public static class RecipeIngredientSectionConverter
    {
        public static RecipeIngredientSectionDto ToDto(
            this RecipeIngredientsSection ingredientsSection )
        {
            return new RecipeIngredientSectionDto
            {
                Title = ingredientsSection.Title,
                Ingredients = ingredientsSection.Ingredients
            };
        }

        public static RecipeIngredientsSection ToRecipeIngredientsSection(
            this RecipeIngredientSectionDto dto )
        {
            return new RecipeIngredientsSection( dto.Title, dto.Ingredients );
        }
    }
}
