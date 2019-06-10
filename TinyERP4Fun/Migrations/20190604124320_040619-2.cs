using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyERP4Fun.Migrations
{
    public partial class _0406192 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Item",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Item");
        }
    }
}
