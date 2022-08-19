using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cookbook_v2.Domain.RecipeModel;

namespace Cookbook_v2.Infrastructure.Data.RecipeModel.Configurations
{
    public class RecipeIngredientsSectionConfiguration : IEntityTypeConfiguration<RecipeIngredientsSection>
    {
        public void Configure( EntityTypeBuilder<RecipeIngredientsSection> builder )
        {
            builder.ToTable( "RecipeIngredientsSection" );
            builder.HasKey( x => x.Id );

            builder.Property( x => x.Title )
                .HasMaxLength( RecipeIngredientsSection.s_titleMaxLength )
                .IsRequired();
        }
    }
}
