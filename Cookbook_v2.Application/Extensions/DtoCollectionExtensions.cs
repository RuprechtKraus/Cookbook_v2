using Cookbook_v2.Application.Converters;
using Cookbook_v2.Application.Dtos.RecipeModel;
using Cookbook_v2.Domain.Entities.RecipeModel;

namespace Cookbook_v2.Application.Extensions
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
    }
}
