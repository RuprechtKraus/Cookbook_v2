using System.ComponentModel.DataAnnotations;
using Cookbook_v2.Application.Dtos.RecipeModel;

namespace Cookbook_v2.Application.Commands.RecipeModel
{
    public class UpdateRecipeCommand
    {
        [Required( ErrorMessage = "Recipe id required" )]
        public int RecipeId { get; set; }

        [Required( ErrorMessage = "User id required" )]
        public int UserId { get; set; }

        [Required( ErrorMessage = "Title required" )]
        [MaxLength( 50, ErrorMessage = "Title maximum length is 50" )]
        public string Title { get; set; } = string.Empty;

        [Required( ErrorMessage = "Description required" )]
        [MaxLength( 1000, ErrorMessage = "Description maximum length is 1000" )]
        public string Description { get; set; } = "";

        [Required( ErrorMessage = "Cooking time required" )]
        public int CookingTimeInMinutes { get; set; }

        [Required( ErrorMessage = "Servings count required" )]
        public int ServingsCount { get; set; }

        public ICollection<RecipeIngredientSectionDto> IngredientsSections { get; set; } =
            new List<RecipeIngredientSectionDto>();
        public ICollection<RecipeStepDto> RecipeSteps { get; set; } =
            new List<RecipeStepDto>();
        public ICollection<string> Tags { get; set; } = new List<string>();
        public string? ImageBase64 { get; set; }
    }
}
