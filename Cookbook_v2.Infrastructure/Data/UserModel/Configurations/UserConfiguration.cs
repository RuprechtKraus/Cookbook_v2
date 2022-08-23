using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cookbook_v2.Domain.UserModel;
using Cookbook_v2.Domain.RecipeModel;

namespace Cookbook_v2.Infrastructure.Data.UserModel.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure( EntityTypeBuilder<User> builder )
        {
            builder.ToTable( "User" );

            builder.HasKey( x => x.Id );
            builder.HasIndex( x => x.Username );

            builder.Property( x => x.Name )
                .HasMaxLength( User.s_nameMaxLength )
                .IsRequired();

            builder.Property( x => x.Username )
                .HasMaxLength( User.s_usernameMaxLength )
                .IsRequired();

            builder.Property( x => x.About )
                .HasMaxLength( User.s_aboutMaxLength );

            builder.Property( x => x.PasswordHash )
                .IsRequired();

            builder.HasMany( x => x.Recipes )
                .WithOne()
                .HasForeignKey( x => x.UserId )
                .OnDelete( DeleteBehavior.ClientCascade );
        }
    }
}
