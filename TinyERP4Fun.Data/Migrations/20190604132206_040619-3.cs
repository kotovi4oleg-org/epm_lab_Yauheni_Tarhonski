using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyERP4Fun.Migrations
{
    public partial class _0406193 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Item");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Item",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Item",
                nullable: true);
        }
    }
}
