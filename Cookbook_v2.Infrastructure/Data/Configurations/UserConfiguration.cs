using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cookbook_v2.Domain.Entities.UserModel;

namespace Cookbook_v2.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure( EntityTypeBuilder<User> builder )
        {
            builder.ToTable( "User" );

            builder.HasKey( x => x.Id );
            builder.HasIndex( x => x.Username );

            builder.Property( x => x.Name )
                .HasMaxLength( 128 )
                .IsRequired();

            builder.Property( x => x.Username )
                .HasMaxLength( 128 )
                .IsRequired();

            builder.Property( x => x.About )
                .HasMaxLength( 1000 )
                .HasDefaultValue( "" )
                .IsRequired();

            builder.Property( x => x.PasswordHash )
                .IsRequired();

            builder.HasMany( x => x.Recipes )
                .WithOne()
                .HasForeignKey( x => x.UserId )
                .OnDelete( DeleteBehavior.ClientCascade );
        }
    }
}
