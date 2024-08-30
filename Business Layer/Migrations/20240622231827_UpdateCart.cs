using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Business_Layer.Migrations
{
    public partial class UpdateCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ShippedDate",
                table: "Order",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 6, 28, 6, 18, 27, 844, DateTimeKind.Local).AddTicks(5536),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 6, 26, 23, 0, 26, 251, DateTimeKind.Local).AddTicks(549));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequiredDate",
                table: "Order",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 6, 26, 6, 18, 27, 844, DateTimeKind.Local).AddTicks(5324),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 6, 24, 23, 0, 26, 251, DateTimeKind.Local).AddTicks(438));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 23, 6, 18, 27, 844, DateTimeKind.Local).AddTicks(5076),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 21, 23, 0, 26, 251, DateTimeKind.Local).AddTicks(246));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ShippedDate",
                table: "Order",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 6, 26, 23, 0, 26, 251, DateTimeKind.Local).AddTicks(549),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 6, 28, 6, 18, 27, 844, DateTimeKind.Local).AddTicks(5536));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequiredDate",
                table: "Order",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 6, 24, 23, 0, 26, 251, DateTimeKind.Local).AddTicks(438),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 6, 26, 6, 18, 27, 844, DateTimeKind.Local).AddTicks(5324));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 6, 21, 23, 0, 26, 251, DateTimeKind.Local).AddTicks(246),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 6, 23, 6, 18, 27, 844, DateTimeKind.Local).AddTicks(5076));
        }
    }
}
