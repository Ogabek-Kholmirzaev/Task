using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskk.Migrations
{
    public partial class SeedProductDatas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "Title");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Price", "Quantity", "Title" },
                values: new object[] { 1, 74.090000000000003, 55, "HDD 1TB" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Price", "Quantity", "Title" },
                values: new object[] { 2, 190.99000000000001, 102, "HDD SSD 512GB" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Price", "Quantity", "Title" },
                values: new object[] { 3, 80.319999999999993, 47, "RAM DDR4 16GB" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Products",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
