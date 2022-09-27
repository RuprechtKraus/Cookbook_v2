using Cookbook_v2.Application.Dtos.RecipeModel;
using Cookbook_v2.Application.Helpers.Converters;
using Cookbook_v2.Application.Services.Interfaces;
using Cookbook_v2.Domain.Entities.RecipeModel;

namespace Cookbook_v2.Application.Dtos.Builders
{
    public class RecipeEditorDtoBuilder
    {
        private readonly IImageService _imageService;

        public RecipeEditorDtoBuilder( IImageService imageService )
        {
            _imageService = imageService;
        }

        public async Task<RecipeEditorDto> Build( Recipe recipe )
        {
            return new RecipeEditorDto
            {
                Title = recipe.Title,
                Description = recipe.Description,
                CookingTimeInMinutes = recipe.CookingTimeInMinutes,
                ServingsCount = recipe.ServingsCount,
                IngredientsSections = recipe.IngredientsSections.ToDtoList(),
                RecipeSteps = recipe.RecipeSteps.ToDtoList(),
                Tags = recipe.Tags.Select( x => x.Name ).ToList(),
                ImageBase64 = await _imageService.EncodeImageToBase64( recipe.ImageName )
            };
        }
    }
}
