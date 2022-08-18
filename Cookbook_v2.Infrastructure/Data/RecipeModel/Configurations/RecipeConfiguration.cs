using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cookbook_v2.Domain.RecipeModel;

namespace Cookbook_v2.Infrastructure.Data.RecipeModel.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure( EntityTypeBuilder<Recipe> builder )
        {
            builder.ToTable( "Recipe" );
            builder.HasKey( x => x.Id );

            builder.Property( x => x.Title )
                .HasMaxLength( Recipe.s_titleMaxLength )
                .IsRequired();

            builder.Property( x => x.Description )
                .HasMaxLength( Recipe.s_descriptionMaxLength )
                .IsRequired();

            builder.Property( x => x.CookingTimeInMinutes )
                .IsRequired();

            builder.Property( x => x.ServingsCount )
                .IsRequired();

            builder.HasMany( x => x.RecipeSteps )
                .WithOne()
                .HasForeignKey( x => x.RecipeId )
                .OnDelete( DeleteBehavior.Cascade );

            builder.HasMany( x => x.IngredientsSections )
                .WithOne()
                .HasForeignKey( x => x.RecipeId )
                .OnDelete( DeleteBehavior.Cascade );
        }
    }
}
