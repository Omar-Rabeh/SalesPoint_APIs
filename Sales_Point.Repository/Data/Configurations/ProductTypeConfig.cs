using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sales_Point.Core.Entities;

namespace Sales_Point.Repository.Data.Configurations
{
    public class ProductTypeConfig : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {

            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        }
    }

}
