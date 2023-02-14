using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskk.Migrations
{
    public partial class TotalPriceWithVAT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalPriceWithVat",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPriceWithVat",
                table: "Products");
        }
    }
}
