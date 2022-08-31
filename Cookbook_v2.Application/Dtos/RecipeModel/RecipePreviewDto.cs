namespace Cookbook_v2.Application.Dtos.RecipeModel
{
    public class RecipePreviewDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public int TimesLiked { get; set; }
        public int TimesFavorited { get; set; }
        public int CookingTimeInMinutes { get; set; }
        public int ServingsCount { get; set; }
        public string ImageName { get; set; } = "";
        public string AuthorUsername { get; set; } = "";
    }
}
