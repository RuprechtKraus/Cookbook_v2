using Cookbook_v2.Application.Dtos.RecipeModel;
using Cookbook_v2.Domain.Entities.RecipeModel;

namespace Cookbook_v2.Application.Converters
{
    public static class RecipeStepConverter
    {
        public static RecipeStepDto ToDto( this RecipeStep recipeStep )
        {
            return new RecipeStepDto
            {
                Index = recipeStep.Index,
                Description = recipeStep.Description
            };
        }

        public static RecipeStep ToRecipeStep( this RecipeStepDto dto )
        {
            return new RecipeStep( dto.Index, dto.Description );
        }
    }
}
