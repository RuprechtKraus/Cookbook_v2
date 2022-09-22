using Microsoft.EntityFrameworkCore;
using Cookbook_v2.Infrastructure.Data.Configurations;

namespace Cookbook_v2.Infrastructure.Data
{
    public class CookbookContext : DbContext
    {
        public CookbookContext( DbContextOptions<CookbookContext> options )
            : base( options )
        {
        }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            base.OnModelCreating( builder );
            builder.ApplyConfiguration( new UserConfiguration() );
            builder.ApplyConfiguration( new RecipeConfiguration() );
            builder.ApplyConfiguration( new RecipeStepConfiguration() );
            builder.ApplyConfiguration( new RecipeIngredientsSectionConfiguration() );
            builder.ApplyConfiguration( new RecipeLikeConfiguration() );
            builder.ApplyConfiguration( new FavoriteRecipeConfiguration() );
            builder.ApplyConfiguration( new CategoryConfiguration() );
            builder.ApplyConfiguration( new TagConfiguration() );
            builder.ApplyConfiguration( new RecipeTagConfiguration() );
        }
    }
}
