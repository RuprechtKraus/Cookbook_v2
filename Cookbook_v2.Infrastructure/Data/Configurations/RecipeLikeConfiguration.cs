using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cookbook_v2.Domain.Entities.RecipeModel;
using Cookbook_v2.Domain.Entities.UserModel;

namespace Cookbook_v2.Infrastructure.Data.Configurations
{
    public class RecipeLikeConfiguration : IEntityTypeConfiguration<RecipeLike>
    {
        public void Configure( EntityTypeBuilder<RecipeLike> builder )
        {
            builder.ToTable( "RecipeLike" );

            builder.HasKey( x => new { x.UserId, x.RecipeId } );

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey( x => x.UserId )
                .OnDelete( DeleteBehavior.Cascade );

            builder.HasOne<Recipe>()
                .WithMany()
                .HasForeignKey( x => x.RecipeId )
                .OnDelete( DeleteBehavior.Cascade );
        }
    }
}
