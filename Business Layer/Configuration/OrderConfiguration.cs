using Data_Layer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(x => x.OrderId);
            builder.Property(x => x.OrderId).ValueGeneratedOnAdd();
            builder.Property(x => x.OrderDate).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.RequiredDate).HasDefaultValue(DateTime.Now.AddDays(3));
            builder.Property(x => x.ShippedDate).HasDefaultValue(DateTime.Now.AddDays(5));
            builder.Property(x => x.Address).HasMaxLength(100);
            builder.Property(x => x.TotalPrice).HasColumnType("money");
            builder.Property(x => x.StatusOrder).HasMaxLength(50);
            builder.HasOne(x => x.User).WithMany(x => x.Orders).HasForeignKey(x => x.MemberId);
           
        }
    }
}
