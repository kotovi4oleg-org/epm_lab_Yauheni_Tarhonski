using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyERP4Fun.Migrations
{
    public partial class _300519NewBase_SomeFixes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Expences");

            migrationBuilder.RenameColumn(
                name: "TransferOfPayment",
                table: "Expences",
                newName: "ApprovedPaymentDate");

            migrationBuilder.AlterColumn<bool>(
                name: "Declined",
                table: "Expences",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Approved",
                table: "Expences",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApprovedPaymentDate",
                table: "Expences",
                newName: "TransferOfPayment");

            migrationBuilder.AlterColumn<bool>(
                name: "Declined",
                table: "Expences",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "Approved",
                table: "Expences",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Expences",
                nullable: true);
        }
    }
}
