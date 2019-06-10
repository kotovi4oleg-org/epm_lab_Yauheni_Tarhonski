using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyERP4Fun.Migrations
{
    public partial class _300519NewBase_SomeFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expences_Company_CompanyId",
                table: "Expences");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransferOfPayment",
                table: "Expences",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDate",
                table: "Expences",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<long>(
                name: "OurCompanyId",
                table: "Expences",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Declined",
                table: "Expences",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                table: "Expences",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Approved",
                table: "Expences",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddForeignKey(
                name: "FK_Expences_Company_CompanyId",
                table: "Expences",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expences_Company_CompanyId",
                table: "Expences");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransferOfPayment",
                table: "Expences",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDate",
                table: "Expences",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "OurCompanyId",
                table: "Expences",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<bool>(
                name: "Declined",
                table: "Expences",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                table: "Expences",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<bool>(
                name: "Approved",
                table: "Expences",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Expences_Company_CompanyId",
                table: "Expences",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
