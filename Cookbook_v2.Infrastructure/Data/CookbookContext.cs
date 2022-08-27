using Microsoft.EntityFrameworkCore;
using Cookbook_v2.Domain.CategoryModel;
using Cookbook_v2.Domain.Entities.UserModel;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Cookbook_v2.Infrastructure.Data.Configurations;

namespace Cookbook_v2.Infrastructure.Data
{
    public class CookbookContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeLike> RecipeLikes { get; set; }
        public DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }
        public DbSet<Category> Categories { get; set; }

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
        }
    }
}
