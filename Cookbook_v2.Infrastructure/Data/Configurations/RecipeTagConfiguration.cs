using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Cookbook_v2.Domain.Entities.TagModel;

namespace Cookbook_v2.Infrastructure.Data.Configurations
{
    public class RecipeTagConfiguration : IEntityTypeConfiguration<RecipeTag>
    {
        public void Configure( EntityTypeBuilder<RecipeTag> builder )
        {
            builder.ToTable( "RecipeTag" );

            builder.HasKey( x => new { x.TagId, x.RecipeId } );

            builder.HasOne<Tag>()
                .WithMany()
                .HasForeignKey( x => x.TagId )
                .OnDelete( DeleteBehavior.Cascade );

            builder.HasOne<Recipe>()
                .WithMany()
                .HasForeignKey( x => x.RecipeId )
                .OnDelete( DeleteBehavior.Cascade );
        }
    }
}
