using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sales_Point.Core.Entities.OrderAggregate;

namespace Sales_Point.Repository.Data.Configurations
{
    internal class DeliveryMethodsConfig : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
             builder.Property(deliveryMethods => deliveryMethods.Cost)
                .HasColumnType("decimal(18,2)");
        }
    }
}
