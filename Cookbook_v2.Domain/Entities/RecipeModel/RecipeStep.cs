namespace Cookbook_v2.Domain.Entities.RecipeModel
{
    public class RecipeStep : EntityBase
    {
        public int RecipeId { get; set; }
        public int Index { get; set; }
        public string Description { get; set; }

        public RecipeStep( int index, string description )
        {
            Index = index;
            Description = description;
        }

        // Workaround for EF
        protected RecipeStep()
        {
        }
    }
}
