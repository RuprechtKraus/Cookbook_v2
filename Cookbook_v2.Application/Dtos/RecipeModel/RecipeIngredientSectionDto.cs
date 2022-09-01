using System.ComponentModel.DataAnnotations;

namespace Cookbook_v2.Application.Dtos.RecipeModel
{
    public class RecipeIngredientSectionDto
    {
        [Required( ErrorMessage = "Title required" )]
        [MaxLength( 50, ErrorMessage = "Title maximum length is 50" )]
        public string Title { get; set; } = "";

        [Required( ErrorMessage = "Ingredients list required" )]
        public string Ingredients { get; set; } = "";
    }
}
