using Data_Layer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Configuration
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("OrderStatus");
            builder.HasKey(x => x.OrderStatusId);
            builder.Property(x => x.OrderStatusId).ValueGeneratedOnAdd();
            builder.Property(x => x.OrderStatusName).HasMaxLength(128);
            builder.Property(x => x.OrderId);
            builder.Property(x => x.ShipperId);

            builder.HasOne(x => x.User).WithMany(x => x.OrderStatuses).HasForeignKey(x => x.ShipperId);
            builder.HasOne(x => x.Order).WithMany(x => x.OrderStatuses).HasForeignKey(x => x.OrderId);
        }
    }
}
