using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cookbook_v2.Domain.RecipeModel;

namespace Cookbook_v2.Infrastructure.Data.RecipeModel.Configurations
{
    public class FavoriteRecipeConfiguration : IEntityTypeConfiguration<FavoriteRecipe>
    {
        public void Configure( EntityTypeBuilder<FavoriteRecipe> builder )
        {
            builder.ToTable( "FavoriteRecipe" );
            builder.HasKey( x => new { x.UserId, x.RecipeId } );

            builder.HasOne( x => x.User )
                .WithMany()
                .HasForeignKey( x => x.UserId )
                .OnDelete( DeleteBehavior.Cascade );

            builder.HasOne( x => x.Recipe )
                .WithMany()
                .HasForeignKey( x => x.RecipeId )
                .OnDelete( DeleteBehavior.Cascade );
        }
    }
}
