using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Entities.Order;

namespace OrderManagement.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.HasMany(o => o.Items)
                   .WithOne()
                   .HasForeignKey("OrderId");

            builder.HasMany(o => o.StatusHistory)
                   .WithOne()
                   .HasForeignKey("OrderId");
        }
    }
}
