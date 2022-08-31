using Cookbook_v2.Application.Converters;
using Cookbook_v2.Application.Dtos.RecipeModel;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Cookbook_v2.Domain.Entities.TagModel;

namespace Cookbook_v2.Application.Helpers.Converters
{
    public static class DtoCollectionExtensions
    {
        public static List<RecipeStep> ToRecipeStepList(
            this ICollection<RecipeStepDto> dtoCollection )
        {
            return dtoCollection.Select( x => x.ToRecipeStep() ).ToList();
        }

        public static List<RecipeIngredientsSection> ToIngredientsSectionList(
            this ICollection<RecipeIngredientSectionDto> dtoCollection )
        {
            return dtoCollection.Select( x => x.ToRecipeIngredientsSection() ).ToList();
        }

        public static List<RecipeStepDto> ToDtoList( this ICollection<RecipeStep> recipeSteps )
        {
            return recipeSteps.Select( x => x.ToDto() ).ToList();
        }

        public static List<RecipeIngredientSectionDto> ToDtoList(
            this ICollection<RecipeIngredientsSection> recipeIngredients )
        {
            return recipeIngredients.Select( x => x.ToDto() ).ToList();
        }
    }
}
