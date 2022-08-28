namespace Cookbook_v2.Domain.Entities.RecipeModel
{
    public class RecipeStep : EntityBase
    {
        public static readonly int s_descriptionMaxLength = 500;
        public int RecipeId { get; set; }
        public int Index { get; set; }
        public string Description { get; set; }

        // Workaround for EF
        protected RecipeStep()
        {
        }
    }
}
