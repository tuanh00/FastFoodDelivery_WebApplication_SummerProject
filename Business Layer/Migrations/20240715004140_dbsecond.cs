using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Business_Layer.Migrations
{
    public partial class dbsecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ShippedDate",
                table: "Order",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 7, 20, 7, 41, 40, 851, DateTimeKind.Local).AddTicks(1370),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 7, 10, 21, 47, 38, 673, DateTimeKind.Local).AddTicks(9595));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequiredDate",
                table: "Order",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 7, 18, 7, 41, 40, 851, DateTimeKind.Local).AddTicks(1169),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 7, 8, 21, 47, 38, 673, DateTimeKind.Local).AddTicks(9366));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 15, 7, 41, 40, 851, DateTimeKind.Local).AddTicks(905),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 5, 21, 47, 38, 673, DateTimeKind.Local).AddTicks(9063));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: new DateTime(2024, 7, 20, 7, 41, 40, 851, DateTimeKind.Local).AddTicks(1370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequiredDate",
                table: "Order",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2024, 7, 8, 21, 47, 38, 673, DateTimeKind.Local).AddTicks(9366),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2024, 7, 18, 7, 41, 40, 851, DateTimeKind.Local).AddTicks(1169));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 5, 21, 47, 38, 673, DateTimeKind.Local).AddTicks(9063),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 15, 7, 41, 40, 851, DateTimeKind.Local).AddTicks(905));
        }
    }
}
