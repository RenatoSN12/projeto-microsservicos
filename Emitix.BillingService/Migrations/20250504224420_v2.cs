using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emitix.BillingService.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductCode",
                table: "InvoiceProduct",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Invoice",
                newName: "InvoiceStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "InvoiceProduct",
                newName: "ProductCode");

            migrationBuilder.RenameColumn(
                name: "InvoiceStatus",
                table: "Invoice",
                newName: "Status");
        }
    }
}
