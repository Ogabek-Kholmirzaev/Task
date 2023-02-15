using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taskk.Migrations
{
    public partial class ProductAudit_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProductAudits",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProductAudits");
        }
    }
}
