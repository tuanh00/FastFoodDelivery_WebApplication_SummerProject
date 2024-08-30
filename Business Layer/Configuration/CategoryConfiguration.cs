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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(x => x.CategoryId);
            builder.Property(x => x.CategoryId).ValueGeneratedOnAdd();
            builder.Property(x => x.CategoriesName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CategoriesStatus).HasMaxLength(100);
        }
    }
}
