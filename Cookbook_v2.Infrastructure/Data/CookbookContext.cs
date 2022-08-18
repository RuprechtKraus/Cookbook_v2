using Microsoft.EntityFrameworkCore;
using Cookbook_v2.Domain.UserModel;
using Cookbook_v2.Domain.RecipeModel;
using Cookbook_v2.Domain.CategoryModel;
using Cookbook_v2.Infrastructure.Data.UserModel.Configurations;

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
        }
    }
}
