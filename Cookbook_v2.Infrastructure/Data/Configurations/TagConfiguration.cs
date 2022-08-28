using Cookbook_v2.Domain.Entities.TagModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cookbook_v2.Infrastructure.Data.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure( EntityTypeBuilder<Tag> builder )
        {
            builder.ToTable( "Tag" );

            builder.HasKey( x => x.Id );
            builder.HasIndex( x => x.Name );

            builder.Property( x => x.Name )
                .HasMaxLength( 20 )
                .IsRequired();
        }
    }
}
