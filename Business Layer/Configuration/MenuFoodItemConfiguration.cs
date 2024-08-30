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
    public class MenuFoodItemConfiguration : IEntityTypeConfiguration<MenuFoodItem>
    {
        public void Configure(EntityTypeBuilder<MenuFoodItem> builder)
        {
            builder.ToTable("MenuFoodItem");
            builder.HasKey(x => x.FoodId);
            builder.Property(x => x.FoodId).ValueGeneratedOnAdd();
            builder.Property(x => x.FoodName).IsRequired().HasMaxLength(100);
            builder.HasOne(x => x.Category).WithMany(x => x.MenuFoodItems).HasForeignKey(x => x.CategoryId);

        }
    }
}
