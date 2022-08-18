using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cookbook_v2.Domain.RecipeStepModel;

namespace Cookbook_v2.Infrastructure.Data.RecipeModel.Configurations
{
    public class RecipeStepConfiguration : IEntityTypeConfiguration<RecipeStep>
    {
        public void Configure( EntityTypeBuilder<RecipeStep> builder )
        {
            builder.ToTable( "RecipeStep" );
            builder.HasKey( x => x.Id );

            builder.Property( x => x.Index )
                .IsRequired();

            builder.Property( x => x.Description )
                .HasMaxLength( RecipeStep.s_descriptionMaxLength )
                .IsRequired();
        }
    }
}
