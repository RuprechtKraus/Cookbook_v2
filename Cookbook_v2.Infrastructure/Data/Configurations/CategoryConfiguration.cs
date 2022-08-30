using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cookbook_v2.Domain.CategoryModel;

namespace Cookbook_v2.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure( EntityTypeBuilder<Category> builder )
        {
            builder.ToTable( "Category" );

            builder.HasKey( x => x.Id );

            builder.Property( x => x.Name )
                .HasMaxLength( 50 )
                .IsRequired();

            builder.Property( x => x.Description )
                .HasMaxLength( 200 )
                .IsRequired();
        }
    }
}
