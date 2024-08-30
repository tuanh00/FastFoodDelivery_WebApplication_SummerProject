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
    public class FeedBackConfiguration : IEntityTypeConfiguration<FeedBack>
    {
        public void Configure(EntityTypeBuilder<FeedBack> builder)
        {
            builder.ToTable("FeedBack");
            builder.HasKey(x => x.FeedBackId);
            builder.Property(x => x.FeedBackId).ValueGeneratedOnAdd();
            builder.Property(x => x.CommentMsg).HasMaxLength(100);

            builder.HasOne(x => x.User).WithMany(x => x.FeedBacks).OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Order).WithMany(x => x.FeedBacks).OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(x => x.OrderId);
        }
    }
}
