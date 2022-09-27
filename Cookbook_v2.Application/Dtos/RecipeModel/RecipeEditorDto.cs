namespace Cookbook_v2.Application.Dtos.RecipeModel
{
    public class RecipeEditorDto
    {
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CookingTimeInMinutes { get; set; }
        public int ServingsCount { get; set; }
        public ICollection<RecipeIngredientSectionDto> IngredientsSections { get; set; } =
            new List<RecipeIngredientSectionDto>();
        public ICollection<RecipeStepDto> RecipeSteps { get; set; } =
            new List<RecipeStepDto>();
        public ICollection<string> Tags { get; set; } = new List<string>();
        public string? ImageBase64 { get; set; }
    }
}
