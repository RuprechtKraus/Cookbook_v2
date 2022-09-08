using System.ComponentModel.DataAnnotations;

namespace Cookbook_v2.Application.Dtos.RecipeModel
{
    public class RecipeStepDto
    {
        [Required( ErrorMessage = "Index required" )]
        public int Index { get; set; }

        [Required( ErrorMessage = "Description required" )]
        [MaxLength( 500, ErrorMessage = "Description maximum length is 500" )]
        public string Description { get; set; } = string.Empty;
    }
}
