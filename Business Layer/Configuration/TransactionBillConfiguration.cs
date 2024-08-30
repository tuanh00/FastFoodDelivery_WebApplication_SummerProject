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
    public class TransactionBillConfiguration : IEntityTypeConfiguration<TransactionBill>
    {
        public void Configure(EntityTypeBuilder<TransactionBill> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(x => x.TractionId);
            builder.Property(x => x.TractionId).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Order).WithMany(x => x.TransactionBills).HasForeignKey(x => x.OrderId);
        }
    }
}
