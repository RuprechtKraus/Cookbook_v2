using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cookbook_v2.Domain.RecipeModel;

namespace Cookbook_v2.Infrastructure.Data.RecipeModel.Configurations
{
    public class RecipeLikeConfiguration : IEntityTypeConfiguration<RecipeLike>
    {
        public void Configure( EntityTypeBuilder<RecipeLike> builder )
        {
            builder.ToTable( "RecipeLike" );
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
