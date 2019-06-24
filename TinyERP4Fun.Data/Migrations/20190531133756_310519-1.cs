using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyERP4Fun.Migrations
{
    public partial class _3105191 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expences_Currency_CurrencyId",
                table: "Expences");

            migrationBuilder.DropForeignKey(
                name: "FK_Expences_DocumentType_DocumentTypeId",
                table: "Expences");

            migrationBuilder.AlterColumn<long>(
                name: "DocumentTypeId",
                table: "Expences",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CurrencyId",
                table: "Expences",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Expences_Currency_CurrencyId",
                table: "Expences",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expences_DocumentType_DocumentTypeId",
                table: "Expences",
                column: "DocumentTypeId",
                principalTable: "DocumentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expences_Currency_CurrencyId",
                table: "Expences");

            migrationBuilder.DropForeignKey(
                name: "FK_Expences_DocumentType_DocumentTypeId",
                table: "Expences");

            migrationBuilder.AlterColumn<long>(
                name: "DocumentTypeId",
                table: "Expences",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "CurrencyId",
                table: "Expences",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Expences_Currency_CurrencyId",
                table: "Expences",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expences_DocumentType_DocumentTypeId",
                table: "Expences",
                column: "DocumentTypeId",
                principalTable: "DocumentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
