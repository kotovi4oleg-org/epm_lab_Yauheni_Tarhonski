using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyERP4Fun.Migrations
{
    public partial class _0406191 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "Stock",
                type: "decimal(18, 4)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Stock");
        }
    }
}
