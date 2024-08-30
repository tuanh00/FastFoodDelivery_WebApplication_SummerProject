using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Business_Layer.Migrations
{
    public partial class AddFieldDeliveryForOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ShippedDate",
                table: "Order",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 7, 10, 21, 47, 38, 673, DateTimeKind.Local).AddTicks(9595),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 6, 28, 6, 18, 27, 844, DateTimeKind.Local).AddTicks(5536));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequiredDate",
                table: "Order",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 7, 8, 21, 47, 38, 673, DateTimeKind.Local).AddTicks(9366),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 6, 26, 6, 18, 27, 844, DateTimeKind.Local).AddTicks(5324));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 5, 21, 47, 38, 673, DateTimeKind.Local).AddTicks(9063),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 23, 6, 18, 27, 844, DateTimeKind.Local).AddTicks(5076));

            migrationBuilder.AddColumn<string>(
                name: "DeliveryStatus",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryStatus",
                table: "Order");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShippedDate",
                table: "Order",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 6, 28, 6, 18, 27, 844, DateTimeKind.Local).AddTicks(5536),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 7, 10, 21, 47, 38, 673, DateTimeKind.Local).AddTicks(9595));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequiredDate",
                table: "Order",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 6, 26, 6, 18, 27, 844, DateTimeKind.Local).AddTicks(5324),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 7, 8, 21, 47, 38, 673, DateTimeKind.Local).AddTicks(9366));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 23, 6, 18, 27, 844, DateTimeKind.Local).AddTicks(5076),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 5, 21, 47, 38, 673, DateTimeKind.Local).AddTicks(9063));
        }
    }
}
